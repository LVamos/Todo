using Microsoft.AspNetCore.Mvc;

using Todo.Services.Abstraction.Services;
using Todo.ViewModels.ViewModels;

namespace Todo.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodoItemsController : ControllerBase
	{
		private readonly ITodoItemService _todoItemService;

		public TodoItemsController(ITodoItemService todoItemService)
		{
			_todoItemService = todoItemService;
		}

		[HttpGet("GetTodoItemsByListId/{listId}")]
		[ProducesResponseType(typeof(TodoItemsViewModel), 200)]
		public async Task<TodoItemsViewModel> GetTodoItemsByListId(int listId)
		{
			try
			{
				return await _todoItemService.GetTodoItemsByListIdAsync(listId);
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpGet("GetTodoItemById/{id}")]
		[ProducesResponseType(typeof(TodoItemViewModel), 200)]
		public async Task<TodoItemViewModel> GetTodoItemById(int id)
		{
			try
			{
				return await _todoItemService.GetTodoItemByIdAsync(id);
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpPost("AddTodoItem")]
		[ProducesResponseType(typeof(ErrorViewModel), 200)]
		public async Task<ErrorViewModel> AddTodoItem([FromBody] TodoItemViewModel todoItem)
		{
			try
			{
				await _todoItemService.AddTodoItemAsync(todoItem);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpPut("UpdateTodoItem")]
		[ProducesResponseType(typeof(ErrorViewModel), 200)]
		public async Task<ErrorViewModel> UpdateTodoItem([FromBody] TodoItemViewModel todoItem)
		{
			try
			{
				await _todoItemService.UpdateTodoItemAsync(todoItem);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpGet("DeleteTodoItem/{id}")]
		[ProducesResponseType(typeof(ErrorViewModel), 200)]
		public async Task<ErrorViewModel> DeleteTodoItem(int id)
		{
			try
			{
				await _todoItemService.DeleteTodoItemAsync(id);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}
	}
}
