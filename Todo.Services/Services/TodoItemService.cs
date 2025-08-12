using FluentValidation;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.Services.Validation;
using Todo.ViewModels.Mapping;


namespace Todo.Services.Services;
public class TodoItemService : ITodoItemService
{
	public async Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId)
	{
		try
		{
            await _validationService.ValidateAndThrowAsync(listId);
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
            await _validationService.ValidateAndThrowAsync(id);
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
		// validations
		try
		{
            await _validationService.ValidateAndThrowAsync(item);
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
		try
		{
            await _validationService.ValidateAndThrowAsync(item);
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
            await _validationService.ValidateAndThrowAsync(id);
            await _databaseService.DeleteTodoItemAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Could not delete a TodoItem {id}", e);
			throw;
		}
	}

    private IValidationService _validationService;
    private ILoggerService _loggerService;
	private IDatabaseService _databaseService;
	private ITodoListService _todoListService;

	public TodoItemService(IValidationService validationService, ILoggerService loggerService, IDatabaseService databaseService, ITodoListService todoListService)
	{
        _validationService = validationService;
        _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
		_databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
		_todoListService = todoListService;
	}
}
