using Autofac;
using Autofac.Extensions.DependencyInjection;

using Todo.App;
using Todo.App.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = ConfigurationHelper.GetConfiguration(builder.Environment);

// using autoFAC instead of standard di
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

Startup startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Register services into Autofac instead of standard DI container
builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

WebApplication app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();
