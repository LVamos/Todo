namespace Todo.App.Helpers;

public static class ConfigurationHelper
{
	public static IConfiguration GetConfiguration(IWebHostEnvironment env)
	{
		IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
			.SetBasePath(env.ContentRootPath)
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables();

		IConfigurationRoot configuration = configurationBuilder.Build();

		string? secretConfigPath = configuration["SecretConfigPath"];
		if (!string.IsNullOrEmpty(secretConfigPath))
		{
			configurationBuilder.AddJsonFile(
				Path.Combine(env.ContentRootPath, secretConfigPath),
				optional: true
			);
			configuration = configurationBuilder.Build();
		}

		return configuration;
	}
}
