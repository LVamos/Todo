using Microsoft.EntityFrameworkCore;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Contracts.Mapping;
using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;


namespace Todo.Services.DatabaseServices;

public class DatabaseService : IDatabaseService
{
	public async Task<bool> TodoListExistsAsync(string listName)
	{
		TodoDbContext context = _contextFactory.GetDbContext();
		return await context.TodoLists.AnyAsync(l => l.Name.ToLower() == listName.ToLower());
	}

	public async Task<List<TodoList>> GetAllTodoListsAsync()
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		List<TodoList> lists = await context.TodoLists.Include(tl => tl.Items).ToListAsync();
        return lists;
	}

	public async Task<TodoList?> GetTodoListByIdAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoList list = await context.TodoLists.Include(tl => tl.Items)
								.FirstAsync(tl => tl.Id == id);
		return list;
	}

	public async Task AddTodoListAsync(TodoList list)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		context.TodoLists.Add(list);
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoListAsync(TodoList list)
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
		if (list == null)
			throw new InvalidOperationException();
		context.TodoLists.Remove(list);
		await context.SaveChangesAsync();
	}

	public async Task<List<TodoItem>> GetTodoItemsByListIdAsync(int listId)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		List<TodoItem> items = await context.TodoItems.Where(ti => ti.TodoListId == listId)
            .ToListAsync();
		if (items == null || !items.Any())
			throw new ArgumentException($"TodoList {listId} not found");
		return items;
	}

	public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoItem item = await context.TodoItems.FirstAsync(ti => ti.Id == id);
		return item;
	}

	public async Task AddTodoItemAsync(TodoItem todoItem)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		context.TodoItems.Add(todoItem);
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoItemAsync(TodoItem item)
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
		if (item == null)
		{
			throw new InvalidOperationException();
		}
		context.TodoItems.Remove(item);
		await context.SaveChangesAsync();
	}

	public DatabaseService(IContextFactory contextFactory)
	{
		_contextFactory = contextFactory;
	}

	private IContextFactory _contextFactory;
}