using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Todo.Dal.Abstraction;
using Todo.Entities.Entities;

namespace Todo.Dal.Context;

public class ContextFactory : IContextFactory
{
	public DbSet<TodoList> TodoLists { get; set; }
	public DbSet<TodoItem> TodoItems { get; set; }

	public ContextFactory(IConfiguration configuration, DbContextOptions<TodoDbContext> dbContextOptions)
	{
		Configuration = configuration;
		DbContextOptions = dbContextOptions;
	}

	private IConfiguration Configuration { get; }
	private DbContextOptions<TodoDbContext> DbContextOptions { get; }

	public TodoDbContext GetDbContext()
	{
		string connectionString = Configuration.GetConnectionString("DefaultConnection");
		return new TodoDbContext(DbContextOptions, connectionString);
	}
}
