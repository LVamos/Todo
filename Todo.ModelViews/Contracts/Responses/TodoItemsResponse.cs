using Todo.Contracts.Contracts.Requests;

namespace Todo.Contracts.Contracts.Responses;
public class TodoItemsResponse : ErrorResponse
{
    public IEnumerable<TodoItemResponse> Items { get; set; }
}
