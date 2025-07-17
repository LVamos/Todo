using Todo.Entities.Entities;

namespace Todo.Services.Abstraction.Services;
public interface ITodoListService
{
	Task<IEnumerable<TodoList>> GetAllTodoListsAsync();
	Task<TodoList?> GetTodoListByIdAsync(int id);
	Task AddTodoListAsync(TodoList list);
	Task UpdateTodoListAsync(TodoList list);
	Task DeleteTodoListAsync(int id);
}
