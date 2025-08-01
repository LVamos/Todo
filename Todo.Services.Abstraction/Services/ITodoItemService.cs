using Todo.ViewModels.ViewModels;

namespace Todo.Services.Abstraction.Services;
public interface ITodoItemService
{
	Task<TodoItemsViewModel> GetTodoItemsByListIdAsync(int listId);
	Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id);
	Task AddTodoItemAsync(TodoItemViewModel item);
	Task UpdateTodoItemAsync(TodoItemViewModel item);
	Task DeleteTodoItemAsync(int id);
}
