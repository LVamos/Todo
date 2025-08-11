using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;


namespace Todo.Services.Abstraction.DatabaseServices
{
	public interface IDatabaseService
	{
		Task<bool> TodoListExistsAsync(string listName);
		Task<TodoListsResponse> GetAllTodoListsAsync();
		Task<TodoListResponse?> GetTodoListByIdAsync(int id);
		Task AddTodoListAsync(AddTodoListRequest todoList);
		Task UpdateTodoListAsync(UpdateTodoListRequest list);
		Task DeleteTodoListAsync(int id);
		Task<TodoItemsResponse> GetTodoItemsByListIdAsync(int listId);
		Task<TodoItemResponse?> GetTodoItemByIdAsync(int id);
		Task AddTodoItemAsync(AddTodoItemRequest item);
		Task UpdateTodoItemAsync(UpdateTodoItemRequest item);
		Task DeleteTodoItemAsync(int id);
	}
}
