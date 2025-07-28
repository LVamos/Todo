using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.ViewModels.ViewModels;

namespace Todo.Services.Services;
public class TodoListService : ITodoListService
{
	public async Task<IEnumerable<TodoListViewModel>> GetAllTodoListsAsync()
	{
		return await _databaseService.GetAllTodoListsAsync();
	}

	public async Task<TodoListViewModel?> GetTodoListByIdAsync(int id)
	{
		return await _databaseService.GetTodoListByIdAsync(id);
	}

	public async Task AddTodoListAsync(TodoListViewModel list)
	{
		await _databaseService.AddTodoListAsync(list);
	}

	public async Task UpdateTodoListAsync(TodoListViewModel list)
	{
		await _databaseService.UpdateTodoListAsync(list);
	}

	public async Task DeleteTodoListAsync(int id)
	{
		await _databaseService.DeleteTodoListAsync(id);
	}

	private IDatabaseService _databaseService;
	private ILoggerService _loggerService;

	public TodoListService(IDatabaseService databaseService, ILoggerService loggerService)
	{
		_databaseService = databaseService;
		_loggerService = loggerService;
	}
}
