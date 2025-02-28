using Microsoft.Extensions.Logging;

using Todo.Services.Abstraction.Services;

namespace Todo.Services.Services;

public class LoggerService : ILoggerService
{
	private readonly ILogger<LoggerService> _logger;

	public LoggerService(ILogger<LoggerService> logger)
	{
		_logger = logger;
	}

	public void LogInfo(string message)
	{
		_logger.LogInformation(message);
	}

	public void LogError(string message)
	{
		_logger.LogError(message);
	}

	public void LogError(string message, Exception exception)
	{
		_logger.LogError($"{message}{Environment.NewLine}{exception}");
	}
}
