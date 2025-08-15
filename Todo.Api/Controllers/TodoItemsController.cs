using Microsoft.AspNetCore.Mvc;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.Services;


namespace Todo.API.Controllers
{
	/// <summary>
	/// API endpoints for working with to-do items within lists.
	/// </summary>
	/// <remarks>
	/// Provides CRUD operations over to-do items. Each action returns an object that may contain an error message in the <c>Error</c> field.
	/// </remarks>
	[ApiController]
	[Route("api/[controller]")]
	public class TodoItemsController : ControllerBase
	{
		private readonly ITodoItemService _todoItemService;

		/// <summary>
		/// Creates a new instance of the to-do items controller.
		/// </summary>
		/// <param name="todoItemService">Application service for working with to-do items.</param>
		public TodoItemsController(ITodoItemService todoItemService)
		{
			_todoItemService = todoItemService;
		}

		/// <summary>
		/// Gets all to-do items that belong to the specified list.
		/// </summary>
		/// <param name="listId">Identifier of the to-do list.</param>
		/// <returns>The list items or error information.</returns>
		/// <response code="200">Returns the collection of items for the given list.</response>
		[HttpGet("GetTodoItemsByListId/{listId}")]
		[ProducesResponseType(typeof(TodoItemsResponse), 200)]
		public async Task<TodoItemsResponse> GetTodoItemsByListId(int listId)
		{
			try
			{
                IdRequest request = new() { Id = listId };
				return await _todoItemService.GetTodoItemsByListIdAsync(request);
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		/// <summary>
		/// Gets details of a specific to-do item by its ID.
		/// </summary>
		/// <param name="id">Identifier of the item.</param>
		/// <returns>Item details or error information.</returns>
		/// <response code="200">Returns the item details.</response>
		[HttpGet("GetTodoItemById/{id}")]
		[ProducesResponseType(typeof(TodoItemResponse), 200)]
		public async Task<TodoItemResponse> GetTodoItemById(int id)
		{
			try
			{
                IdRequest request = new() { Id = id };
				return await _todoItemService.GetTodoItemByIdAsync(request);
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}

		/// <summary>
		/// Adds a new to-do item into a list.
		/// </summary>
		/// <param name="item">Data for the new item.</param>
		/// <returns>Empty object on success or error information.</returns>
		/// <response code="200">The item was successfully added.</response>
		[HttpPost("AddTodoItem")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> AddTodoItem([FromBody] AddTodoItemRequest item)
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

		/// <summary>
		/// Updates an existing to-do item.
		/// </summary>
		/// <param name="item">Updated item data.</param>
		/// <returns>Empty object on success or error information.</returns>
		/// <response code="200">The item was successfully updated.</response>
		[HttpPut("UpdateTodoItem")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> UpdateTodoItem([FromBody] UpdateTodoItemRequest item)
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

		/// <summary>
		/// Deletes a to-do item by its ID.
		/// </summary>
		/// <param name="id">Identifier of the item to delete.</param>
		/// <returns>Empty object on success or error information.</returns>
		/// <response code="200">The item was successfully deleted.</response>
		[HttpGet("DeleteTodoItem/{id}")]
		[ProducesResponseType(typeof(ErrorResponse), 200)]
		public async Task<ErrorResponse> DeleteTodoItem(int id)
		{
			try
			{
                IdRequest request = new() { Id = id };
				await _todoItemService.DeleteTodoItemAsync(request);
				return new();
			}
			catch (Exception e)
			{
				return new() { Error = e.ToString() };
			}
		}
	}
}
