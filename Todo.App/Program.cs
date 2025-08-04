using Autofac.Extensions.DependencyInjection;

using Serilog;

using Todo.App;
using Todo.App.Helpers;
using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.ServerConfigurations.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
IConfiguration configuration = ConfigurationHelper.GetConfiguration(builder.Environment);

// using autoFAC instead of standard di
builder.Host.UseServiceProviderFactory(
	new AutofacServiceProviderFactory(x => new IocMapping().Install(configuration, x)));

Startup startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);

WebApplication app = builder.Build();
IocContainerProvider.ServiceProvider = app.Services;
IContextFactory factory = app.Services.GetService<IContextFactory>();
using (TodoDbContext dbContext = factory.GetDbContext())
{
	dbContext.Database.EnsureCreated();
}

startup.Configure(app, app.Environment);

app.Run();
