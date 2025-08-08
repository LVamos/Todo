using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using System.Text;

using Todo.App;
using Todo.App.Helpers;
using Todo.Constants.Settings;
using Todo.ViewModels.ViewModels;

namespace Todo.Tests.Tests;
[SetUpFixture]
public class TestPlatform
{
    public static async Task<HttpResponseMessage> PostAsync(object obj, String uri)
    {
        string json = JsonConvert.SerializeObject(obj);
        StringContent content = new(json, Encoding.UTF8,
            "application/json");
        return await TestPlatform.Client.PostAsync(uri, content);
    }

    public static async Task<HttpResponseMessage> PutAsync(object obj, String uri)
    {
        string json = JsonConvert.SerializeObject(obj);
        StringContent content = new(json, Encoding.UTF8,
            "application/json");
        return await TestPlatform.Client.PutAsync(uri, content);
    }

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