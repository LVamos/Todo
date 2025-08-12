using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;


namespace Todo.Services.Abstraction.DatabaseServices
{
	public interface IDatabaseService
	{
		Task<bool> TodoListExistsAsync(string listName);
		Task<TodoListsResponse> GetAllTodoListsAsync();
		Task<TodoListResponse?> GetTodoListByIdAsync(IdRequest id);
		Task AddTodoListAsync(AddTodoListRequest todoList);
		Task UpdateTodoListAsync(UpdateTodoListRequest list);
		Task DeleteTodoListAsync(IdRequest id);
		Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId);
		Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id);
		Task AddTodoItemAsync(AddTodoItemRequest item);
		Task UpdateTodoItemAsync(UpdateTodoItemRequest item);
		Task DeleteTodoItemAsync(IdRequest id);
	}
}
