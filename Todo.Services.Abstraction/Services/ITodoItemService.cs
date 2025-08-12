using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;


namespace Todo.Services.Abstraction.Services;
public interface ITodoItemService
{
	Task<TodoItemsResponse> GetTodoItemsByListIdAsync(IdRequest listId);
	Task<TodoItemResponse?> GetTodoItemByIdAsync(IdRequest id);
	Task AddTodoItemAsync(AddTodoItemRequest item);
	Task UpdateTodoItemAsync(UpdateTodoItemRequest item);
	Task DeleteTodoItemAsync(IdRequest id);
}
