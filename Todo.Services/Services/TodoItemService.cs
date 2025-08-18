using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Contracts.Mapping;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;

namespace Todo.Services.Services;
public class TodoItemService : ITodoItemService
{
    public async Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId)
    {
        List<Entities.Entities.TodoItem> items = await _databaseService.GetTodoItemsByListIdAsync(listId.ToId());
        return items.ToResponse();
    }

    public async Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id)
    {
        TodoItem? item =await _databaseService.GetTodoItemByIdAsync(id.ToId());
        return item.ToResponse();
    }

    public async Task AddTodoItemAsync(AddTodoItemRequest item)
        => await _databaseService.AddTodoItemAsync(item.ToEntity());

    public async Task UpdateTodoItemAsync(UpdateTodoItemRequest item)
        => await _databaseService.UpdateTodoItemAsync(item.ToEntity());

    public async Task DeleteTodoItemAsync(IdRequest id)
        => await _databaseService.DeleteTodoItemAsync(id.ToId());

    private readonly IDatabaseService _databaseService;

    public TodoItemService(IDatabaseService databaseService, ILoggerService loggerService)
    {
        _databaseService = databaseService;
        // loggerService intentionally unused: logging moved to global ExceptionHandlingMiddleware
    }
}