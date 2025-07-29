using Autofac;

using System.Text.Json.Serialization;

using Todo.Api;

namespace Todo.App;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
		});

		services.AddControllers()
			.AddApplicationPart(typeof(AssemblyMarker).Assembly) // Set location of controllers
			.AddJsonOptions(options =>
			{
				// Adding JSON serialization with support for cyclic references
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
			});

	}

	public void Configure(WebApplication app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI();

			// Redirect from "/" to "/swagger"
			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/")
				{
					context.Response.Redirect("/swagger");
					return;
				}
				await next();
			});
		}

		app.UseHttpsRedirection();

		// Správné pořadí middleware
		app.UseRouting();
		app.UseAuthorization();
		app.MapControllers();
	}

}
