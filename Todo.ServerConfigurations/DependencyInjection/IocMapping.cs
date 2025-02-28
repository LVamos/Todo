using Autofac;

using Microsoft.Extensions.Configuration;

using Todo.Constants.Settings;
using Todo.Services.Abstraction.Services;
using Todo.Services.Services;

namespace Todo.ServerConfigurations.DependencyInjection;

/// <summary>
///     Maps services for dependency injection.
/// </summary>
public class IocMapping
{
	protected void RegisterCommunicationServices(ContainerBuilder builder)
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
	}

	/// <summary>
	///     Registers services related to configurations.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected void RegisterConfigurationServices(IConfiguration configuration, ContainerBuilder builder)
	{
		builder.Register(x => configuration.GetSection(TodoSettings.SectionName).Get<TodoSettings>())
			.As<TodoSettings>();
	}

	/// <summary>
	///     Registers background services.
	/// </summary>
	/// <param name="builder">A container builder</param>
	private void RegisterBackgroundServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Registers services related to business logic.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected void RegisterBusinessServices(ContainerBuilder builder)
	{
		builder.RegisterType<LoggerService>().As<ILoggerService>();
	}
}