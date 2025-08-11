using Todo.Contracts.Contracts.Responses;

namespace Todo.Contracts.Contracts.Requests;

public class UpdateTodoListRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
