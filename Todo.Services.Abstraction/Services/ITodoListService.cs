using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;


namespace Todo.Services.Abstraction.Services;
public interface ITodoListService
{
	Task<TodoListsResponse> GetAllTodoListsAsync();
	Task<TodoListResponse?> GetTodoListByIdAsync(int id);
	Task AddTodoListAsync(TodoListRequest list);
	Task UpdateTodoListAsync(TodoListRequest list);
	Task DeleteTodoListAsync(int id);
}
