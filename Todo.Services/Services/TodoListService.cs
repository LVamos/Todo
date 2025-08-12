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

	public async Task<TodoListResponse?> GetTodoListByIdAsync(IdRequest id)
	{
		try
		{
            await _validationService.ValidateAndThrowAsync(id);
         
            return await _databaseService.GetTodoListByIdAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during finding TodoList {id}", e);
			throw;
		}
	}

	public async Task AddTodoListAsync(AddTodoListRequest list)
	{
		if (await _databaseService.TodoListExistsAsync(list.Name))
		{
			string message = $"TodoList named {list.Name} already exists";
			InvalidOperationException exception = new(message);
			_loggerService.LogError(message, exception);
			throw exception;
		}

		try
		{
            await _validationService.ValidateAndThrowAsync(list);
            await _databaseService.AddTodoListAsync(list);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Unable to add new TodoList", e);
			throw;
		}
	}

	public async Task UpdateTodoListAsync(UpdateTodoListRequest list)
	{
		try
		{
            await _validationService.ValidateAndThrowAsync(list);
            await _databaseService.UpdateTodoListAsync(list);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during updating Todolist {list.Id}", e);
			throw;
		}
	}

	public async Task DeleteTodoListAsync(IdRequest id)
	{
		try
		{
            await _validationService.ValidateAndThrowAsync(id);
            await _databaseService.DeleteTodoListAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during deletion of TodoList {id}", e);
			throw;
		}
	}

    private IValidationService _validationService;
    private IDatabaseService _databaseService;
	private ILoggerService _loggerService;

	public TodoListService(IValidationService validationService, IDatabaseService databaseService, ILoggerService loggerService)
	{
        _validationService = validationService;
        _databaseService = databaseService;
		_loggerService = loggerService;
	}
}
