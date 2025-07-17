using Todo.Entities.Entities;

namespace Todo.Services.Abstraction.Services;
public interface ITodoItemService
{
	Task<IEnumerable<TodoItem>> GetTodoItemsByListIdAsync(int listId);
	Task<TodoItem?> GetTodoItemByIdAsync(int id);
	Task AddTodoItemAsync(TodoItem item);
	Task UpdateTodoItemAsync(TodoItem item);
	Task DeleteTodoItemAsync(int id);
}
