using Microsoft.AspNetCore.Mvc;

using Todo.Services.Abstraction.Services;
using Todo.ViewModels.ViewModels;

namespace Todo.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodoListsController : ControllerBase
	{
		private readonly ITodoListService _todoListService;

		public TodoListsController(ITodoListService todoListService)
		{
			_todoListService = todoListService;
		}

		[HttpGet("GetAllTodoLists")]
		[ProducesResponseType(typeof(TodoListsViewModel), 200)]
		public async Task<TodoListsViewModel> GetAllTodoLists()
		{
			try
			{
				return await _todoListService.GetAllTodoListsAsync();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpGet("GetTodoListById/{id}")]
		[ProducesResponseType(typeof(TodoListViewModel), 200)]
		public async Task<TodoListViewModel> GetTodoListById(int id)
		{
			try
			{
				return await _todoListService.GetTodoListByIdAsync(id);
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpPost("AddTodoList")]
		[ProducesResponseType(typeof(ErrorViewModel), 201)]
		public async Task<ErrorViewModel> AddTodoList([FromBody] TodoListViewModel todoList)
		{
			try
			{
				await _todoListService.AddTodoListAsync(todoList);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpPut("UpdateTodoList/{id}")]
		[ProducesResponseType(typeof(ErrorViewModel), 200)]
		public async Task<ErrorViewModel> UpdateTodoList(int id, [FromBody] TodoListViewModel todoList)
		{
			if (id != todoList.Id)
				return new()
				{
					Error = "Bad request"
				};

			try
			{
				await _todoListService.UpdateTodoListAsync(todoList);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpGet("DeleteTodoList/{id}")]
		[ProducesResponseType(typeof(ErrorViewModel), 200)]
		public async Task<ErrorViewModel> DeleteTodoList(int id)
		{
			try
			{
				await _todoListService.DeleteTodoListAsync(id);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}
	}
}
