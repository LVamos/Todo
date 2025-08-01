using Todo.Services.Abstraction.DatabaseServices;
using Todo.Services.Abstraction.Services;
using Todo.ViewModels.ViewModels;

namespace Todo.Services.Services;
public class TodoListService : ITodoListService
{
	public async Task<TodoListsViewModel> GetAllTodoListsAsync()
	{
		try
		{
			return await _databaseService.GetAllTodoListsAsync();
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during optaining all TodoLists", e);
			throw;
		}
	}

	public async Task<TodoListViewModel?> GetTodoListByIdAsync(int id)
	{
		try
		{
			return await _databaseService.GetTodoListByIdAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during finding TodoList {id}", e);
			throw;
		}
	}

	public async Task AddTodoListAsync(TodoListViewModel list)
	{
		try
		{
			await _databaseService.AddTodoListAsync(list);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Unable to add new TodoList", e);
			throw;
		}
	}

	public async Task UpdateTodoListAsync(TodoListViewModel list)
	{
		try
		{
			await _databaseService.UpdateTodoListAsync(list);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during updating Todolist {list.Id}", e);
			throw;
		}
	}

	public async Task DeleteTodoListAsync(int id)
	{
		try
		{
			await _databaseService.DeleteTodoListAsync(id);
		}
		catch (Exception e)
		{
			_loggerService.LogError($"Error during deletion of TodoList {id}", e);
			throw;
		}
	}

	private IDatabaseService _databaseService;
	private ILoggerService _loggerService;

	public TodoListService(IDatabaseService databaseService, ILoggerService loggerService)
	{
		_databaseService = databaseService;
		_loggerService = loggerService;
	}
}
