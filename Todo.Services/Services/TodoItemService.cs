using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.ViewModels.ViewModels;

namespace Todo.Services.Services;
public class TodoItemService : ITodoItemService
{
	public async Task<IEnumerable<TodoItemViewModel>> GetTodoItemsByListIdAsync(int listId)
	{
		return await _databaseService.GetTodoItemsByListIdAsync(listId);
	}

	public async Task<TodoItemViewModel?> GetTodoItemByIdAsync(int id)
	{
		return await _databaseService.GetTodoItemByIdAsync(id);
	}

	public async Task AddTodoItemAsync(TodoItemViewModel item)
	{
		await _databaseService.AddTodoItemAsync(item);
	}

	public async Task UpdateTodoItemAsync(TodoItemViewModel item)
	{
		await _databaseService.UpdateTodoItemAsync(item);
	}

	public async Task DeleteTodoItemAsync(int id)
	{
		await _databaseService.DeleteTodoItemAsync(id);
	}


	private ILoggerService _loggerService;
	private IDatabaseService _databaseService;

	public TodoItemService(ILoggerService loggerService, IDatabaseService databaseService)
	{
		_loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
		_databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
	}
}
