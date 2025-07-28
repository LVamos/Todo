using Todo.ViewModels.ViewModels;

namespace Todo.Services.Abstraction.DatabaseServices
{
	public interface IDatabaseService
	{
		Task<IEnumerable<TodoListViewModel>> GetAllTodoListsAsync();
		Task<TodoListViewModel?> GetTodoListByIdAsync(int id);
		Task AddTodoListAsync(TodoListViewModel todoList);
		Task UpdateTodoListAsync(TodoListViewModel todoList);
		Task DeleteTodoListAsync(int id);
		Task<IEnumerable<TodoItemViewModel>> GetTodoItemsByListIdAsync(int todoListId);
		Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id);
		Task AddTodoItemAsync(TodoItemViewModel todoItem);
		Task UpdateTodoItemAsync(TodoItemViewModel todoItem);
		Task DeleteTodoItemAsync(int id);
	}
}
