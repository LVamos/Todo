using FluentValidation;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.ViewModels.Mapping;


namespace Todo.Services.Services;
public class TodoItemService : ITodoItemService
{
	public async Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId)
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

	public async Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id)
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

	public async Task AddTodoItemAsync(AddTodoItemRequest item)
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
			TodoListResponse parentList = await _todoListService.GetTodoListByIdAsync(item.TodoListId.ToContract());
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

	public async Task UpdateTodoItemAsync(UpdateTodoItemRequest item)
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

	public async Task DeleteTodoItemAsync(IdRequest id)
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

    private IValidator<IdRequest> _idRequestValidator;
    private ILoggerService _loggerService;
	private IDatabaseService _databaseService;
	private ITodoListService _todoListService;

	public TodoItemService(IValidator<IdRequest> idRequestValidator, ILoggerService loggerService, IDatabaseService databaseService, ITodoListService todoListService)
	{
        _idRequestValidator = idRequestValidator;
		_loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
		_databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
		_todoListService = todoListService;
	}
}
