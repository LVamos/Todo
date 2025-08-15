using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;

namespace Todo.Services.Services;
public class TodoItemService : ITodoItemService
{
    public async Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId)
        => await _databaseService.GetTodoItemsByListIdAsync(listId);

    public async Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id)
        => await _databaseService.GetTodoItemByIdAsync(id);

    public async Task AddTodoItemAsync(AddTodoItemRequest item)
        => await _databaseService.AddTodoItemAsync(item);

    public async Task UpdateTodoItemAsync(UpdateTodoItemRequest item)
        => await _databaseService.UpdateTodoItemAsync(item);

    public async Task DeleteTodoItemAsync(IdRequest id)
        => await _databaseService.DeleteTodoItemAsync(id);

    private readonly IDatabaseService _databaseService;

    public TodoItemService(IDatabaseService databaseService, ILoggerService loggerService)
    {
        _databaseService = databaseService;
        // loggerService intentionally unused: logging moved to global ExceptionHandlingMiddleware
    }
}