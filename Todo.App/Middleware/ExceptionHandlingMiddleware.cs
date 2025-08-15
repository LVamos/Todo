using System.Net;

using FluentValidation;

namespace Todo.App.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, logLevel, responseObj) = MapException(ex);

            // Log with appropriate level
            switch (logLevel)
            {
                case LogLevel.Warning:
                    _logger.LogWarning(ex, "{ExceptionType} handled: {Message}", ex.GetType().Name, ex.Message);
                    break;
                case LogLevel.Information:
                    _logger.LogInformation(ex, "{ExceptionType} handled: {Message}", ex.GetType().Name, ex.Message);
                    break;
                default:
                    _logger.LogError(ex, "Unhandled exception occurred.");
                    break;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(responseObj);
            }
        }
    }

    private static (int StatusCode, LogLevel LogLevel, object Response) MapException(Exception ex) =>
        ex switch
        {
            ValidationException v => (
                StatusCodes.Status400BadRequest,
                LogLevel.Warning,
                new
                {
                    type = "validation",
                    error = "Validation failed",
                    details = v.Errors
                        .Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                        .GroupBy(e => e.field)
                        .ToDictionary(g => g.Key, g => g.Select(x => x.message).ToArray())
                }),

            InvalidOperationException => (
                StatusCodes.Status409Conflict,
                LogLevel.Warning,
                new
                {
                    type = "conflict",
                    error = ex.Message
                }),

            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                LogLevel.Information,
                new
                {
                    type = "not_found",
                    error = ex.Message
                }),

            _ => (
                StatusCodes.Status500InternalServerError,
                LogLevel.Error,
                new
                {
                    type = "internal",
                    error = "An unexpected error occurred.",
                    detail = ex.Message // keep simple; avoid inner exception chain leakage
                })
        };
}