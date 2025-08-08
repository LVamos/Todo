using Todo.Contracts.Contracts.Requests;

namespace Todo.Contracts.Contracts.Responses;
public class TodoListsResponse : ErrorResponse
{
    public List<TodoListResponse> Lists { get; set; }
}
