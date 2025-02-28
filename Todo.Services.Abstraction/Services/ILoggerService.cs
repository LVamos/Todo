namespace Todo.Services.Abstraction.Services;

public interface ILoggerService
{
	void LogInfo(string message);
	void LogError(string message);
	void LogError(string message, Exception exception);
}

