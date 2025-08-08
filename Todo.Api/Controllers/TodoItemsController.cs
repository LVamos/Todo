using Microsoft.AspNetCore.Mvc;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.Services;


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
		[ProducesResponseType(typeof(TodoItemsResponse), 200)]
		public async Task<TodoItemsResponse> GetTodoItemsByListId(int listId)
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
		[ProducesResponseType(typeof(TodoItemResponse), 200)]
		public async Task<TodoItemResponse> GetTodoItemById(int id)
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
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> AddTodoItem([FromBody] TodoItemRequest item)
		{
			try
			{
				await _todoItemService.AddTodoItemAsync(item);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpPut("UpdateTodoItem")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> UpdateTodoItem([FromBody] TodoItemRequest item)
		{
			try
			{
				await _todoItemService.UpdateTodoItemAsync(item);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpGet("DeleteTodoItem/{id}")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> DeleteTodoItem(int id)
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
