using Todo.Contracts.Contracts.Responses;
using Todo.Entities.Entities;

namespace Todo.Contracts.Contracts.Requests;

public class UpdateTodoItemRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
    public DateTime? Deadline { get; set; }
    public string Comment { get; set; }
    public int TodoListId { get; set; }
}
