using Autofac;
using Autofac.Extensions.DependencyInjection;
using Todo.ServerConfigurations.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Autofac integration
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>((ctx, container) =>
{
    new IocMapping().Install(ctx.Configuration, container);
});

// Controllers from the separate library (Todo.Api)
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(Todo.API.Controllers.TodoListsController).Assembly);

// Swagger + XML comments
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // XML from API project
    var apiAssembly = typeof(Todo.API.Controllers.TodoListsController).Assembly;
    var apiXml = Path.Combine(AppContext.BaseDirectory, $"{apiAssembly.GetName().Name}.xml");
    if (File.Exists(apiXml))
    {
        options.IncludeXmlComments(apiXml, includeControllerXmlComments: true);
    }

    // XML from host project (optional)
    var hostXml = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    if (File.Exists(hostXml))
    {
        options.IncludeXmlComments(hostXml);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();