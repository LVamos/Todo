using Todo.ViewModels.ViewModels;

namespace Todo.Services.Abstraction.DatabaseServices
{
	public interface IDatabaseService
	{
		Task<bool> TodoListExists(string listName);
		Task<TodoListsViewModel> GetAllTodoListsAsync();
		Task<TodoListViewModel?> GetTodoListByIdAsync(int id);
		Task AddTodoListAsync(TodoListViewModel todoList);
		Task UpdateTodoListAsync(TodoListViewModel list);
		Task DeleteTodoListAsync(int id);
		Task<TodoItemsViewModel> GetTodoItemsByListIdAsync(int listId);
		Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id);
		Task AddTodoItemAsync(TodoItemViewModel todoItem);
		Task UpdateTodoItemAsync(TodoItemViewModel item);
		Task DeleteTodoItemAsync(int id);
	}
}
