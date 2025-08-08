using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;


namespace Todo.Services.Abstraction.DatabaseServices
{
	public interface IDatabaseService
	{
		Task<bool> TodoListExistsAsync(string listName);
		Task<TodoListsResponse> GetAllTodoListsAsync();
		Task<TodoListResponse?> GetTodoListByIdAsync(int id);
		Task AddTodoListAsync(TodoListRequest todoList);
		Task UpdateTodoListAsync(TodoListRequest list);
		Task DeleteTodoListAsync(int id);
		Task<TodoItemsResponse> GetTodoItemsByListIdAsync(int listId);
		Task<TodoItemResponse?> GetTodoItemByIdAsync(int id);
		Task AddTodoItemAsync(TodoItemRequest item);
		Task UpdateTodoItemAsync(TodoItemRequest item);
		Task DeleteTodoItemAsync(int id);
	}
}
