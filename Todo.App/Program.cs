using Autofac.Extensions.DependencyInjection;

using Todo.App;
using Todo.App.Helpers;
using Todo.ServerConfigurations.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = ConfigurationHelper.GetConfiguration(builder.Environment);

// using autoFAC instead of standard di
builder.Host.UseServiceProviderFactory(
	new AutofacServiceProviderFactory(x => new IocMapping().Install(configuration, x)));

Startup startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);

WebApplication app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();
