using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

using Todo.App;
using Todo.App.Helpers;
using Todo.Tests.DependencyInjection;

namespace Todo.Tests;
public class TestEntrypoint
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
		IConfiguration configuration = ConfigurationHelper.GetConfiguration(builder.Environment);

		// using autoFAC instead of standard di
		builder.Host.UseServiceProviderFactory(
			new AutofacServiceProviderFactory(x => new TestIocMapping().Install(configuration, x)));

		Startup startup = new(builder.Configuration);
		startup.ConfigureServices(builder.Services);

		WebApplication app = builder.Build();
		IocContainerProvider.ServiceProvider = app.Services;
		startup.Configure(app, app.Environment);
		app.Run();
	}
}
