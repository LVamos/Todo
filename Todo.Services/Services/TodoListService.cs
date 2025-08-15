using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;

namespace Todo.Services.Services;
public class TodoListService : ITodoListService
{
	public async Task<TodoListsResponse> GetAllTodoListsAsync() => await _databaseService.GetAllTodoListsAsync();

	public async Task<TodoListResponse?> GetTodoListByIdAsync(IdRequest id)
	{
        await _validationService.ValidateAndThrowAsync(id);
        return await _databaseService.GetTodoListByIdAsync(id);
	}

	public async Task AddTodoListAsync(AddTodoListRequest list)
	{
		if (await _databaseService.TodoListExistsAsync(list.Name))
			throw new InvalidOperationException($"TodoList named {list.Name} already exists");

        await _validationService.ValidateAndThrowAsync(list);
        await _databaseService.AddTodoListAsync(list);
	}

	public async Task UpdateTodoListAsync(UpdateTodoListRequest list)
	{
        await _validationService.ValidateAndThrowAsync(list);
        await _databaseService.UpdateTodoListAsync(list);
	}

	public async Task DeleteTodoListAsync(IdRequest id)
	{
        await _validationService.ValidateAndThrowAsync(id);
        await _databaseService.DeleteTodoListAsync(id);
	}

    private readonly IValidationService _validationService;
    private readonly IDatabaseService _databaseService;

	public TodoListService(IValidationService validationService, IDatabaseService databaseService, ILoggerService loggerService)
	{
        _validationService = validationService;
        _databaseService = databaseService;
        // loggerService intentionally unused: logging moved to global ExceptionHandlingMiddleware
	}
}
