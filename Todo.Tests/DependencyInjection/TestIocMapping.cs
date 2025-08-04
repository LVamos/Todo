using Autofac;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Todo.Constants.Settings;
using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.ServerConfigurations.DependencyInjection;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.Services.DatabaseServices;
using Todo.Services.Services;

namespace Todo.Tests.DependencyInjection;
public class TestIocMapping : IocMapping
{
	protected new void RegisterCommunicationServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Registers services related to server communication.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected new void RegisterServerServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Installs a DI container.
	/// </summary>
	/// <param name="builder">A container builder</param>
	public new void Install(IConfiguration configuration, ContainerBuilder builder)
	{
		RegisterBackgroundServices(builder);
		RegisterDatabaseServices(configuration, builder);
		RegisterConfigurationServices(configuration, builder);
		RegisterCommunicationServices(builder);
		RegisterServerServices(builder);
		RegisterBusinessServices(builder);
	}

	protected new void RegisterDatabaseServices(IConfiguration configuration, ContainerBuilder builder)
	{
		string connectionString = configuration.GetConnectionString("DefaultConnection");
		builder.Register(ctx =>
		{
			DbContextOptionsBuilder<TodoDbContext> optionsBuilder = new();
			optionsBuilder.UseSqlServer(connectionString);
			return optionsBuilder.Options;
		}).As<DbContextOptions<TodoDbContext>>().SingleInstance();

		builder.RegisterType<ContextFactory>().As<IContextFactory>();
		builder.RegisterType<DatabaseService>().As<IDatabaseService>();
	}

	/// <summary>
	///     Registers services related to configurations.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected new void RegisterConfigurationServices(IConfiguration configuration, ContainerBuilder builder)
	{
		builder.Register(x => configuration.GetSection(TodoSettings.SectionName).Get<TodoSettings>())
			.As<TodoSettings>();
	}

	/// <summary>
	///     Registers background services.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected new void RegisterBackgroundServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Registers services related to business logic.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected new void RegisterBusinessServices(ContainerBuilder builder)
	{
		builder.RegisterType<LoggerService>().As<ILoggerService>();
		builder.RegisterType<TodoListService>().As<ITodoListService>();
		builder.RegisterType<TodoItemService>().As<ITodoItemService>();
	}
}
