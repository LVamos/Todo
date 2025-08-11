using Microsoft.AspNetCore.Mvc;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.Services;

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
		[ProducesResponseType(typeof(TodoListsResponse), 200)]
		public async Task<TodoListsResponse> GetAllTodoLists()
		{
			try
			{
				TodoListsResponse result = await _todoListService.GetAllTodoListsAsync();
				return result;
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		[HttpGet("GetTodoListById/{id}")]
		[ProducesResponseType(typeof(TodoListResponse), 200)]
		public async Task<TodoListResponse> GetTodoListById(int id)
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
		[ProducesResponseType(typeof(ErrorResponse), 201)]
		public async Task<ErrorResponse> AddTodoList([FromBody] AddTodoListRequest todoList)
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

		[HttpPut("UpdateTodoList")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> UpdateTodoList([FromBody] UpdateTodoListRequest todoList)
		{
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
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> DeleteTodoList(int id)
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
