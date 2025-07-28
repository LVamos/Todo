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

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<TodoListViewModel>), 200)]
		public async Task<ActionResult<IEnumerable<TodoListViewModel>>> GetAllTodoLists()
		{
			IEnumerable<TodoListViewModel> lists = await _todoListService.GetAllTodoListsAsync();
			return Ok(lists);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(TodoListViewModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<TodoListViewModel>> GetTodoListById(int id)
		{
			TodoListViewModel? list = await _todoListService.GetTodoListByIdAsync(id);
			if (list == null)
				return NotFound();
			return Ok(list);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		public async Task<ActionResult> AddTodoList([FromBody] TodoListViewModel todoList)
		{
			await _todoListService.AddTodoListAsync(todoList);
			return CreatedAtAction(nameof(GetTodoListById), new { id = todoList.Id }, todoList);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> UpdateTodoList(int id, [FromBody] TodoListViewModel todoList)
		{
			if (id != todoList.Id)
				return BadRequest();
			await _todoListService.UpdateTodoListAsync(todoList);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeleteTodoList(int id)
		{
			await _todoListService.DeleteTodoListAsync(id);
			return NoContent();
		}
	}
}
