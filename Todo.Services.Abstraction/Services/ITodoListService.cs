using Todo.ViewModels.ViewModels;

namespace Todo.Services.Abstraction.Services;
public interface ITodoListService
{
	Task<TodoListsViewModel> GetAllTodoListsAsync();
	Task<TodoListViewModel?> GetTodoListByIdAsync(int id);
	Task AddTodoListAsync(TodoListViewModel list);
	Task UpdateTodoListAsync(TodoListViewModel list);
	Task DeleteTodoListAsync(int id);
}
