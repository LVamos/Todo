using Autofac;

using System.Text.Json.Serialization;

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
		services.AddControllers();
		services.AddSwaggerGen();

		// Adding JSON serialization with support for cyclic references
		services.AddControllers().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
		});
	}

	public void ConfigureContainer(ContainerBuilder builder)
	{
		// Registering services in Autofac instead of standard DI
		//builder.RegisterType<MyService>().As<IMyService>().InstancePerLifetimeScope();
		//builder.RegisterType<AnotherService>().As<IAnotherService>().SingleInstance();
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
		app.UseAuthorization();
		app.MapControllers();
		app.UseRouting();
	}
}
