namespace Todo.Entities.Entities;

public sealed class TodoList
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public ICollection<TodoItem> Items { get; set; } = [];
}
