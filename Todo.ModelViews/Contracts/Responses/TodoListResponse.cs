namespace Todo.Contracts.Contracts.Responses;

public class TodoListResponse : ErrorResponse
{
	public List<TodoItemResponse> Items { get; set; } = [];
	public int Id { get; set; }
	public string Name { get; set; }
}
