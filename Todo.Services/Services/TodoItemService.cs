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
        await _validationService.ValidateAndThrowAsync(listId);
        return await _databaseService.GetTodoItemsByListIdAsync(listId);
	}

	public async Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id)
	{
        await _validationService.ValidateAndThrowAsync(id);
		return await _databaseService.GetTodoItemByIdAsync(id);
	}

	public async Task AddTodoItemAsync(AddTodoItemRequest item)
	{
        await _validationService.ValidateAndThrowAsync(item);
        // ensure parent list exists
        TodoListResponse parentList = await _todoListService.GetTodoListByIdAsync(item.TodoListId.ToContract());
        if (parentList == null)
            throw new ArgumentException($"Parent TodoList {item.TodoListId} not found");
		await _databaseService.AddTodoItemAsync(item);
	}

	public async Task UpdateTodoItemAsync(UpdateTodoItemRequest item)
	{
        await _validationService.ValidateAndThrowAsync(item);
        await _databaseService.UpdateTodoItemAsync(item);
	}

	public async Task DeleteTodoItemAsync(IdRequest id)
	{
        await _validationService.ValidateAndThrowAsync(id);
        await _databaseService.DeleteTodoItemAsync(id);
	}

    private readonly IValidationService _validationService;
    private readonly IDatabaseService _databaseService;
	private readonly ITodoListService _todoListService;

	public TodoItemService(IValidationService validationService, ILoggerService loggerService, IDatabaseService databaseService, ITodoListService todoListService)
	{
        _validationService = validationService;
        _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
		_todoListService = todoListService;
        // loggerService intentionally unused: logging moved to global ExceptionHandlingMiddleware
	}
}
