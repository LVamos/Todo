using Microsoft.AspNetCore.Mvc;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.Services;
using Todo.ViewModels.Mapping;

namespace Todo.API.Controllers
{
	/// <summary>
	/// API endpoints for working with to-do lists.
	/// </summary>
	/// <remarks>
	/// Provides CRUD operations for to-do lists. Every response may contain an error message in the <c>Error</c> field.
	/// </remarks>
	[ApiController]
	[Route("api/[controller]")]
	public class TodoListsController : ControllerBase
	{
		private readonly ITodoListService _todoListService;

		/// <summary>
		/// Creates a new instance of the to-do lists controller.
		/// </summary>
		/// <param name="todoListService">Application service for working with lists.</param>
		public TodoListsController(ITodoListService todoListService)
		{
			_todoListService = todoListService;
		}

		/// <summary>
		/// Retrieves all available to-do lists.
		/// </summary>
		/// <returns>A collection of lists or error information.</returns>
		/// <response code="200">Returns all available lists.</response>
    
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

		/// <summary>
		/// Retrieves a specific to-do list by its ID.
		/// </summary>
		/// <param name="id">Identifier of the list.</param>
		/// <returns>List details or error information.</returns>
		/// <response code="200">Returns the list details.</response>
		[HttpGet("GetTodoListById/{id}")]
		[ProducesResponseType(typeof(TodoListResponse), 200)]
		public async Task<TodoListResponse> GetTodoListById(int id)
		{
			try
			{
                return await _todoListService.GetTodoListByIdAsync(id.ToContract());
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		/// <summary>
		/// Creates a new to-do list.
		/// </summary>
		/// <param name="todoList">Data of the new list.</param>
		/// <returns>Empty object on success or error information.</returns>
		/// <response code="201">The list was successfully created.</response>
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

		/// <summary>
		/// Updates an existing to-do list.
		/// </summary>
		/// <param name="todoList">Updated list data.</param>
		/// <returns>Empty object on success or error information.</returns>
		/// <response code="200">The list was successfully updated.</response>
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

		/// <summary>
		/// Deletes a to-do list by its ID.
		/// </summary>
		/// <param name="id">Identifier of the list to delete.</param>
		/// <returns>Empty object on success or error information.</returns>
		/// <response code="200">The list was successfully deleted.</response>
		[HttpGet("DeleteTodoList/{id}")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> DeleteTodoList(int id)
		{
			try
			{
                IdRequest request = new() { Id = id };
				await _todoListService.DeleteTodoListAsync(request);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}
	}
}
