using Autofac;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;

using Todo.Constants.Settings;
using Todo.Contracts.Contracts.Requests;
using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.Services.DatabaseServices;
using Todo.Services.Services;
using Todo.Services.Validation;

namespace Todo.ServerConfigurations.DependencyInjection;

/// <summary>
///     Maps services for dependency injection.
/// </summary>
public class IocMapping
{
	protected virtual void RegisterCommunicationServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Registers services related to server communication.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected virtual void RegisterServerServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Installs a DI container.
	/// </summary>
	/// <param name="builder">A container builder</param>
	public virtual void Install(IConfiguration configuration, ContainerBuilder builder)
	{
		RegisterBackgroundServices(builder);
		RegisterDatabaseServices(configuration, builder);
		RegisterConfigurationServices(configuration, builder);
		RegisterCommunicationServices(builder);
		RegisterServerServices(builder);
		RegisterBusinessServices(builder);
	}

	protected virtual void RegisterDatabaseServices(IConfiguration configuration, ContainerBuilder builder)
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
	protected virtual void RegisterConfigurationServices(IConfiguration configuration, ContainerBuilder builder)
	{
		builder.Register(x => configuration.GetSection(TodoSettings.SectionName).Get<TodoSettings>())
			.As<TodoSettings>();
	}

	/// <summary>
	///     Registers background services.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected virtual void RegisterBackgroundServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Registers services related to business logic.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected virtual void RegisterBusinessServices(ContainerBuilder builder)
	{
        builder.RegisterAssemblyTypes(typeof(IdRequestValidator).Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterType<ValidationService>()
       .As<IValidationService>()
       .SingleInstance();
        builder.RegisterType<LoggerService>().As<ILoggerService>();
		builder.RegisterType<TodoListService>().As<ITodoListService>();
		builder.RegisterType<TodoItemService>().As<ITodoItemService>();
	}
}