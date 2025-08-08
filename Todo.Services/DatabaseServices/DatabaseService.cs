using Microsoft.EntityFrameworkCore;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.Entities.Entities;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.ViewModels.Mapping;


namespace Todo.Services.DatabaseServices;

public class DatabaseService : IDatabaseService
{
	public async Task<bool> TodoListExistsAsync(string listName)
	{
		TodoDbContext context = _contextFactory.GetDbContext();
		return await context.TodoLists.AnyAsync(l => l.Name.ToLower() == listName.ToLower());
	}

	public async Task<TodoListsResponse> GetAllTodoListsAsync()
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		List<TodoList> lists = await context.TodoLists.Include(tl => tl.Items).ToListAsync();
		return lists.ToResponses();
	}

	public async Task<TodoListResponse?> GetTodoListByIdAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoList list = await context.TodoLists.Include(tl => tl.Items)
								.FirstAsync(tl => tl.Id == id);
		return list.ToResponse();
	}

	public async Task AddTodoListAsync(TodoListRequest todoList)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		context.TodoLists.Add(todoList.ToEntity());
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoListAsync(TodoListRequest list)
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

	public async Task<TodoItemsResponse> GetTodoItemsByListIdAsync(int listId)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		List<TodoItem> items = await context.TodoItems.Where(ti => ti.TodoListId == listId).ToListAsync();
		if (items == null || !items.Any())
			throw new ArgumentException($"TodoList {listId} not found");
		return items.ToResponse();
	}

	public async Task<TodoItemResponse?> GetTodoItemByIdAsync(int id)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		TodoItem item = await context.TodoItems.FirstAsync(ti => ti.Id == id);
		return item.ToResponse();
	}

	public async Task AddTodoItemAsync(TodoItemRequest todoItem)
	{
		using TodoDbContext context = _contextFactory.GetDbContext();
		context.TodoItems.Add(todoItem.ToEntity());
		await context.SaveChangesAsync();
	}

	public async Task UpdateTodoItemAsync(TodoItemRequest item)
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