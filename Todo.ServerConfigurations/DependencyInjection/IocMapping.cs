namespace Todo.ServerConfigurations.DependencyInjection;

/// <summary>
///     Maps services for dependency injection.
/// </summary>
public class IocMapping
{
	protected void RegisterCommunicationServices(ContainerBuilder builder)
	{
		builder.RegisterType<DigitooAuthorizationSetter>().As<IDigitooAuthorizationSetter>();
		builder.RegisterType<VarioadapterAuthorizationSetter>().As<IVarioadapterAuthorizationSetter>();
		builder.RegisterType<DigitooAuthorizationSetterV2>().As<IDigitooAuthorizationSetterV2>();
		builder.RegisterType<K2AdapterAuthorizationSetter>().As<IK2AdapterAuthorizationSetter>();
		builder.RegisterType<DigitooHeaderSetter>().As<IDigitooHeaderSetter>();
		builder.RegisterType<CommunicationClient>().As<ICommunicationClient>();
		builder.RegisterType<CommunicationClientProvider>().As<ICommunicationClientProvider>();
	}

	/// <summary>
	///     Registers services related to server communication.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected virtual void RegisterServerServices(ContainerBuilder builder)
	{
		builder.RegisterType<VarioAdapterServerService>().As<IVarioAdapterServerService>();
		builder.RegisterType<K2AdapterServerService>().As<IK2AdapterServerService>();
		builder.RegisterType<DigitooServerService>().As<IDigitooServerService>();
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
		builder.RegisterInstance(new DbContextOptions<DigitooAdapterDbContext>());
		builder.RegisterType<ContextFactory>().As<IContextFactory>();
		builder.RegisterType<DatabaseService>().As<IDatabaseService>();
	}

	/// <summary>
	///     Registers services related to configurations.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected void RegisterConfigurationServices(IConfiguration configuration, ContainerBuilder builder)
	{
		builder.Register(x => configuration.GetSection(DigitooSettings.SectionName).Get<DigitooSettings>()!)
			.As<DigitooSettings>();
	}

	/// <summary>
	///     Registers background services.
	/// </summary>
	/// <param name="builder">A container builder</param>
	private void RegisterBackgroundServices(ContainerBuilder builder)
	{
		builder.RegisterType<K2ExporterService>().As<IStartable>().SingleInstance();
		builder.RegisterType<WorkspaceUpdaterService>().As<IStartable>().SingleInstance();
	}

	/// <summary>
	///     Registers services related to business logic.
	/// </summary>
	/// <param name="builder">A container builder</param>
	protected void RegisterBusinessServices(ContainerBuilder builder)
	{
		builder.RegisterType<RegisterService>().As<IRegisterService>();
		builder.RegisterType<AutomaticK2ExportSettingsService>().As<IAutomaticK2ExportSettingsService>();
		builder.RegisterType<RegisteredWorkspaceService>().As<IRegisteredWorkspaceService>();
		builder.RegisterType<LoggerService>().As<ILoggerService>();
		builder.RegisterType<TestService>().As<ITestService>();
		builder.RegisterType<DigitooService>().As<IDigitooService>();
	}
}