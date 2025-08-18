using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;


namespace Todo.Services.Abstraction.DatabaseServices
{
	public interface IDatabaseService
	{
		Task<bool> TodoListExistsAsync(string listName);
		Task<List<TodoList>> GetAllTodoListsAsync();
		Task<TodoList?> GetTodoListByIdAsync(int id);
		Task AddTodoListAsync(TodoList list);
		Task UpdateTodoListAsync(TodoList list);
		Task DeleteTodoListAsync(int id);
		Task<List<TodoItem>> GetTodoItemsByListIdAsync(int listId);
		Task<TodoItem?> GetTodoItemByIdAsync(int id);
		Task AddTodoItemAsync(TodoItem item);
		Task UpdateTodoItemAsync(TodoItem item);
		Task DeleteTodoItemAsync(int id);
	}
}
