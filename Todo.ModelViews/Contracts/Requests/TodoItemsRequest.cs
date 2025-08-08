using Todo.Contracts.Contracts.Requests;

namespace Todo.Contracts.Contracts.Responses;
public class TodoItemsRequest
{
    public IEnumerable<TodoItemRequest> Items { get; set; }
}
