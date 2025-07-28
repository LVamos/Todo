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

		[HttpGet("list/{listId}")]
		[ProducesResponseType(typeof(IEnumerable<TodoItemViewModel>), 200)]
		public async Task<ActionResult<IEnumerable<TodoItemViewModel>>> GetTodoItemsByListId(int listId)
		{
			IEnumerable<TodoItemViewModel> items = await _todoItemService.GetTodoItemsByListIdAsync(listId);
			return Ok(items);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(TodoItemViewModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<TodoItemViewModel>> GetTodoItemById(int id)
		{
			TodoItemViewModel? item = await _todoItemService.GetTodoItemByIdAsync(id);
			if (item == null)
				return NotFound();
			return Ok(item);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		public async Task<ActionResult> AddTodoItem([FromBody] TodoItemViewModel todoItem)
		{
			await _todoItemService.AddTodoItemAsync(todoItem);
			return CreatedAtAction(nameof(GetTodoItemById), new { id = todoItem.Id }, todoItem);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> UpdateTodoItem(int id, [FromBody] TodoItemViewModel todoItem)
		{
			if (id != todoItem.Id)
				return BadRequest();
			await _todoItemService.UpdateTodoItemAsync(todoItem);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeleteTodoItem(int id)
		{
			await _todoItemService.DeleteTodoItemAsync(id);
			return NoContent();
		}
	}
}
