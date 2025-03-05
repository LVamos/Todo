using Microsoft.EntityFrameworkCore;

using Todo.Entities.Entities;

namespace Todo.Dal.Context;

public class TodoDbContext : DbContext
{
	public TodoDbContext(DbContextOptions<TodoDbContext> options, string connectionString) : base(options)
	{
		_connectionString = connectionString;
	}

	public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
	{
	}
	private string _connectionString { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<TodoList>()
			.HasMany(tl => tl.Items)
			.WithOne(ti => ti.TodoList)
			.HasForeignKey(ti => ti.TodoListId);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(_connectionString);
	}
}
