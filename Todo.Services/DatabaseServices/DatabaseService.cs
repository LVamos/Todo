using Microsoft.EntityFrameworkCore;

using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;

namespace Todo.Services.DatabaseServices;

public class DatabaseService : IDatabaseService
{
	public async Task<IEnumerable<TodoList>> GetAllTodoListsAsync()
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		List<TodoList> lists = await context.TodoLists.Include(tl => tl.Items).ToListAsync();
		return lists;
	}

	public async Task<TodoList?> GetTodoListByIdAsync(int id)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		TodoList list = await context.TodoLists.Include(tl => tl.Items)
								.FirstOrDefaultAsync(tl => tl.Id == id);
		return list;
	}

	public async Task AddTodoListAsync(TodoList todoList)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		context.TodoLists.Add(todoList);
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoListAsync(TodoList todoList)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		context.TodoLists.Update(todoList);
		await context.SaveChangesAsync();
	}

	public async Task DeleteTodoListAsync(int id)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		TodoList list = await context.TodoLists.FindAsync(id);
		if (list != null)
		{
			context.TodoLists.Remove(list);
			await context.SaveChangesAsync();
		}
	}

	public async Task<IEnumerable<TodoItem>> GetTodoItemsByListIdAsync(int todoListId)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		List<TodoItem> items = await context.TodoItems.Where(ti => ti.TodoListId == todoListId).ToListAsync();
		return items;
	}

	public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		TodoItem item = await context.TodoItems.FirstOrDefaultAsync(ti => ti.Id == id);
		return item;
	}

	public async Task AddTodoItemAsync(TodoItem todoItem)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		context.TodoItems.Add(todoItem);
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoItemAsync(TodoItem todoItem)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		context.TodoItems.Update(todoItem);
		await context.SaveChangesAsync();
	}

	public async Task DeleteTodoItemAsync(int id)
	{
		using TodoDbContext context = contextFactory.GetDbContext();
		TodoItem item = await context.TodoItems.FindAsync(id);
		if (item != null)
		{
			context.TodoItems.Remove(item);
			await context.SaveChangesAsync();
		}
	}

	public DatabaseService(IContextFactory contextFactory)
	{
		contextFactory = contextFactory;
	}

	private IContextFactory contextFactory;
}