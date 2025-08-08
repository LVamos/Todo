using Todo.Contracts.Contracts.Responses;

namespace Todo.Contracts.Contracts.Requests;

public class TodoListRequest
{
	public List<TodoItemRequest> Items { get; set; } = [];
	public int Id { get; set; }
	public string Name { get; set; }
}
