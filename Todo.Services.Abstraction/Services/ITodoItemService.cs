using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;


namespace Todo.Services.Abstraction.Services;
public interface ITodoItemService
{
	Task<TodoItemsResponse> GetTodoItemsByListIdAsync(int listId);
	Task<TodoItemResponse?> GetTodoItemByIdAsync(int id);
	Task AddTodoItemAsync(AddTodoItemRequest item);
	Task UpdateTodoItemAsync(UpdateTodoItemRequest item);
	Task DeleteTodoItemAsync(int id);
}
