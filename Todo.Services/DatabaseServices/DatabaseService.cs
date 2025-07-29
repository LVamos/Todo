using Microsoft.EntityFrameworkCore;

using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.ViewModels.Mapping;
using Todo.ViewModels.ViewModels;

namespace Todo.Services.DatabaseServices;

public class DatabaseService : IDatabaseService
{
	public async Task<IEnumerable<TodoListViewModel>> GetAllTodoListsAsync()
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		List<TodoList> lists = await context.TodoLists.Include(tl => tl.Items).ToListAsync();
		return lists.ToViewModels();
	}

	public async Task<TodoListViewModel?> GetTodoListByIdAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoList list = await context.TodoLists.Include(tl => tl.Items)
								.FirstOrDefaultAsync(tl => tl.Id == id);
		return list.ToViewModel();
	}

	public async Task AddTodoListAsync(TodoListViewModel todoList)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		context.TodoLists.Add(todoList.ToEntity());
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoListAsync(TodoListViewModel list)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoList existing = await context.TodoLists.FindAsync(list.Id);
		if (existing == null)
			throw new ArgumentException();
		existing.Name = list.Name;
		await context.SaveChangesAsync();
	}

	public async Task DeleteTodoListAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoList list = await context.TodoLists.FindAsync(id);
		if (list != null)
		{
			context.TodoLists.Remove(list);
			await context.SaveChangesAsync();
		}
	}

	public async Task<IEnumerable<TodoItemViewModel>> GetTodoItemsByListIdAsync(int todoListId)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		List<TodoItem> items = await context.TodoItems.Where(ti => ti.TodoListId == todoListId).ToListAsync();
		return items.ToViewModels();
	}

	public async Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoItem item = await context.TodoItems.FirstOrDefaultAsync(ti => ti.Id == id);
		return item.ToViewModel();
	}

	public async Task AddTodoItemAsync(TodoItemViewModel todoItem)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		context.TodoItems.Add(todoItem.ToEntity());
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoItemAsync(TodoItemViewModel item)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoItem existing = await context.TodoItems.FindAsync(item.Id);
		if (existing == null)
			throw new ArgumentException();

		existing.Title = item.Title;
		existing.Priority = item.Priority;
		existing.Deadline = item.Deadline;
		existing.Comment = item.Comment;
		existing.IsCompleted = item.IsCompleted;
		await context.SaveChangesAsync();
	}

	public async Task DeleteTodoItemAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoItem item = await context.TodoItems.FindAsync(id);
		if (item != null)
		{
			context.TodoItems.Remove(item);
			await context.SaveChangesAsync();
		}
	}

	public DatabaseService(IContextFactory contextFactory)
	{
		_contextFactory = contextFactory;
	}

	private IContextFactory _contextFactory;
}