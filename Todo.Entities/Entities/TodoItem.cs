namespace Todo.Entities.Entities;

public enum Priority
{
	Low,
	Medium,
	High
}

public sealed class TodoItem
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public bool IsCompleted { get; set; }
	public Priority Priority { get; set; }
	public DateTime? Deadline { get; set; }
	public string Comment { get; set; }

	// Cizí klíč
	public int TodoListId { get; set; }
	public TodoList TodoList { get; set; }
}
