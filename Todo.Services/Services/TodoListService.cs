using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.Contracts.Mapping;

namespace Todo.Services.Services;
public class TodoListService : ITodoListService
{
    public async Task<TodoListsResponse> GetAllTodoListsAsync()
    {
        List<TodoList> lists = await _databaseService.GetAllTodoListsAsync();
        return lists.ToResponses();
    }

    public async Task<TodoListResponse?> GetTodoListByIdAsync(IdRequest id)
    {
        TodoList list = await _databaseService.GetTodoListByIdAsync(id.ToId());
        return list.ToResponse();
    }

    public async Task AddTodoListAsync(AddTodoListRequest list)
    {
        if (await _databaseService.TodoListExistsAsync(list.Name))
            throw new InvalidOperationException($"TodoList named {list.Name} already exists");

         await _databaseService.AddTodoListAsync(list.ToEntity() );
    }

    public async Task UpdateTodoListAsync(UpdateTodoListRequest list)
        => await _databaseService.UpdateTodoListAsync(list.ToEntity());

    public async Task DeleteTodoListAsync(IdRequest id)
        => await _databaseService.DeleteTodoListAsync(id.ToId());

    private readonly ITodoRepository _databaseService;

    public TodoListService(ITodoRepository databaseService, ILoggerService loggerService)
    {
        _databaseService = databaseService;
        // loggerService intentionally unused: logging moved to global ExceptionHandlingMiddleware
    }
}