using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.ViewModels.ViewModels;

namespace Todo.Services.Services;
public class TodoItemService : ITodoItemService
{
	public async Task<TodoItemsViewModel> GetTodoItemsByListIdAsync(int listId)
	{
		try
		{
			return await _databaseService.GetTodoItemsByListIdAsync(listId);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Could not find TodoItems for TodoList {listId}", e);
			throw;
		}
	}

	public async Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id)
	{
		try
		{
			return await _databaseService.GetTodoItemByIdAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"TodoItem {id} not found", e);
			throw;
		}
	}

	public async Task AddTodoItemAsync(TodoItemViewModel item)
	{
		// Validate title
		if (string.IsNullOrEmpty(item.Title))
		{
			ArgumentNullException exception = new(nameof(item.Title));
			_loggerService.LogError($"TodoItem must have a title", exception);
			throw exception;
		}

		// validate TodoList link
		try
		{
			TodoListViewModel parentList = await _todoListService.GetTodoListByIdAsync(item.TodoListId);
		}
		catch (Exception)
		{
			_loggerService.LogError($"Adding TodoItem failed: TodoList {item.TodoListId} not found or couldn't be retrieved from database");
			throw;
		}

		try
		{
			await _databaseService.AddTodoItemAsync(item);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Unable to add TodoItem {item.Title}", e);
			throw;
		}
	}

	public async Task UpdateTodoItemAsync(TodoItemViewModel item)
	{
		// Validate title
		if (string.IsNullOrEmpty(item.Title))
		{
			ArgumentNullException exception = new(nameof(item.Title));
			_loggerService.LogError($"TodoItem must have a title", exception);
			throw exception;
		}

		try
		{
			await _databaseService.UpdateTodoItemAsync(item);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Unable to update TodoItem {item.Id}", e);
			throw;
		}
	}

	public async Task DeleteTodoItemAsync(int id)
	{
		try
		{
			await _databaseService.DeleteTodoItemAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Could not delete a TodoItem {id}", e);
			throw;
		}
	}


	private ILoggerService _loggerService;
	private IDatabaseService _databaseService;
	private ITodoListService _todoListService;

	public TodoItemService(ILoggerService loggerService, IDatabaseService databaseService, ITodoListService todoListService)
	{
		_loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
		_databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
		_todoListService = todoListService;
	}
}
