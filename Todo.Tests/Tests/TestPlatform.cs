using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Todo.App;
using Todo.App.Helpers;
using Todo.Constants.Settings;

namespace Todo.Tests.Tests;
[SetUpFixture]
public class TestPlatform
{
	public static HttpClient Client;
	public static TestServer Server;

	[OneTimeSetUp]
	public void OneTimeSetup()
	{
		var builder = WebApplication.CreateBuilder();
		var configuration = ConfigurationHelper.GetConfiguration(builder.Environment);

		WebApplicationFactory<TestEntrypoint> application = new();
		Server = application.Server;
		Client = application.CreateClient();
		TodoSettings settings = IocContainerProvider.ServiceProvider.GetService<TodoSettings>();
		//todo Client.DefaultRequestHeaders.Add("Authorization", settings.DevlopApiKey);
	}

	[OneTimeTearDown]
	public void OneTimeTearDown()
	{
		Client?.Dispose();
		Server?.Dispose();
	}
}