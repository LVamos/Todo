using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;


namespace Todo.Services.Services;
public class TodoListService : ITodoListService
{
	public async Task<TodoListsResponse> GetAllTodoListsAsync()
	{
		try
		{
			return await _databaseService.GetAllTodoListsAsync();
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during optaining all TodoLists", e);
			throw;
		}
	}

	public async Task<TodoListResponse?> GetTodoListByIdAsync(int id)
	{
		try
		{
			return await _databaseService.GetTodoListByIdAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during finding TodoList {id}", e);
			throw;
		}
	}

	public async Task AddTodoListAsync(TodoListRequest list)
	{
		if (string.IsNullOrEmpty(list.Name))
		{
			ArgumentNullException exception = new(nameof(list.Name));
			_loggerService.LogError("TodoList must have a name");
			throw exception;
		}

		if (await _databaseService.TodoListExistsAsync(list.Name))
		{
			string message = $"TodoList named {list.Name} already exists";
			InvalidOperationException exception = new(message);
			_loggerService.LogError(message, exception);
			throw exception;
		}

		if (list.Items?.Count > 0)
		{
			InvalidOperationException exception = new("Cannot create TodoItems before the TodoList is saved.");
			_loggerService.LogError($"Cannot create TodoItems before the TodoList is saved");
			throw exception;
		}

		try
		{
			await _databaseService.AddTodoListAsync(list);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Unable to add new TodoList", e);
			throw;
		}
	}

	public async Task UpdateTodoListAsync(TodoListRequest list)
	{
		if (string.IsNullOrEmpty(list.Name))
		{
			ArgumentNullException exception = new(nameof(list.Name));
			_loggerService.LogError("TodoList must have a name");
			throw exception;
		}

		try
		{
			await _databaseService.UpdateTodoListAsync(list);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during updating Todolist {list.Id}", e);
			throw;
		}
	}

	public async Task DeleteTodoListAsync(int id)
	{
		try
		{
			await _databaseService.DeleteTodoListAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during deletion of TodoList {id}", e);
			throw;
		}
	}

	private IDatabaseService _databaseService;
	private ILoggerService _loggerService;

	public TodoListService(IDatabaseService databaseService, ILoggerService loggerService)
	{
		_databaseService = databaseService;
		_loggerService = loggerService;
	}
}
