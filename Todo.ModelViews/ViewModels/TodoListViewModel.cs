namespace Todo.ViewModels.ViewModels;

public class TodoListViewModel
{
	public List<TodoItemViewModel> Items { get; set; } = [];
	public int Id { get; set; }
	public string Name { get; set; }
}
