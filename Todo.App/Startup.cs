using Microsoft.AspNetCore.Mvc;
using Autofac;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Todo.Services.Validation;

namespace Todo.App;
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson();

        // FluentValidation v11+ integration (AddFluentValidation removed)
        services.AddValidatorsFromAssemblyContaining<AddTodoListValidator>();
        services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true; // optional
        });
        services.AddFluentValidationClientsideAdapters();

        // ... další DI registrace ...
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.MapControllers();
    }
}