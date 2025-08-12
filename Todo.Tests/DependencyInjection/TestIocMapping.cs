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
using Todo.Tests.Mocks;

namespace Todo.Tests.DependencyInjection;
public class TestIocMapping : IocMapping
{
	protected override void RegisterCommunicationServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Registers services related to server communication.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected override void RegisterServerServices(ContainerBuilder builder)
	{
	}

	/// <summary>
	///     Installs a DI container.
	/// </summary>
	/// <param name="builder">A container builder</param>
	public override void Install(IConfiguration configuration, ContainerBuilder builder)
	{
		RegisterConfigurationServices(configuration, builder);
		RegisterBackgroundServices(builder);
		RegisterDatabaseServices(configuration, builder);
		RegisterCommunicationServices(builder);
		RegisterServerServices(builder);
		RegisterBusinessServices(builder);
	}

	protected override void RegisterDatabaseServices(IConfiguration configuration, ContainerBuilder builder)
	{
		builder.RegisterType<TestDatabaseService>().As<IDatabaseService>();
	}

	/// <summary>
	///     Registers services related to configurations.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected override void RegisterConfigurationServices(IConfiguration configuration, ContainerBuilder builder)
	{
		builder.Register(x => configuration.GetSection(TodoSettings.SectionName).Get<TodoSettings>())
			.As<TodoSettings>();
	}

	/// <summary>
	///     Registers background services.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected override void RegisterBackgroundServices(ContainerBuilder builder)
	{
	}

}
