using Todo.Entities.Entities;

namespace Todo.Services.Abstraction.DatabaseServices;

public interface IDatabaseService
{
	Task<IEnumerable<TodoList>> GetAllTodoListsAsync();
	Task<TodoList?> GetTodoListByIdAsync(int id);
	Task AddTodoListAsync(TodoList todoList);
	Task UpdateTodoListAsync(TodoList todoList);
	Task DeleteTodoListAsync(int id);

	// Operace s TodoItem
	Task<IEnumerable<TodoItem>> GetTodoItemsByListIdAsync(int todoListId);
	Task<TodoItem?> GetTodoItemByIdAsync(int id);
	Task AddTodoItemAsync(TodoItem todoItem);
	Task UpdateTodoItemAsync(TodoItem todoItem);
	Task DeleteTodoItemAsync(int id);
}