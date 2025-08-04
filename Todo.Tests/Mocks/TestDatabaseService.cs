using Todo.Services.Abstraction.DatabaseServices;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Mocks;

public class TestDatabaseService : IDatabaseService
{
	public async Task<bool> TodoListExists(string listName) => false;

	public async Task<TodoListsViewModel> GetAllTodoListsAsync() => new TodoListsViewModel();

	public async Task<TodoListViewModel?> GetTodoListByIdAsync(int id) => null;

	public async Task AddTodoListAsync(TodoListViewModel todoList) { }

	public async Task UpdateTodoListAsync(TodoListViewModel list) { }

	public async Task DeleteTodoListAsync(int id) { }

	public async Task<TodoItemsViewModel> GetTodoItemsByListIdAsync(int listId) => new TodoItemsViewModel();

	public async Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id) => null;

	public async Task AddTodoItemAsync(TodoItemViewModel todoItem) { }

	public async Task UpdateTodoItemAsync(TodoItemViewModel item) { }

	public async Task DeleteTodoItemAsync(int id) { }
}
