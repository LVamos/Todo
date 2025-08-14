using Autofac.Extensions.DependencyInjection;
using Serilog;
using Todo.App;
using Todo.App.Helpers;
using Todo.Dal.Abstraction;
using Todo.Dal.Context;
using Todo.ServerConfigurations.DependencyInjection;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
IConfiguration configuration = ConfigurationHelper.GetConfiguration(builder.Environment);

// using Autofac instead of standard DI
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory(x => new IocMapping().Install(configuration, x)));

Startup startup = new(builder.Configuration);
startup.ConfigureServices(builder.Services);

WebApplication app = builder.Build();
IocContainerProvider.ServiceProvider = app.Services;

// Apply EF Core migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IContextFactory>();
    using var dbContext = factory.GetDbContext();
    dbContext.Database.Migrate();
}

startup.Configure(app, app.Environment);

app.Run();
