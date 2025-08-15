using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.Services;

namespace Todo.API.Controllers
{
    /// <summary>
    /// API endpoints for working with to-do lists.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListsController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListsController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        /// <summary>Gets a to-do list by ID.</summary>
        [HttpGet("GetTodoListById/{id}")]
        [ProducesResponseType(typeof(TodoListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoListResponse>> GetTodoListById([FromRoute] IdRequest request)
        {
            TodoListResponse result = await _todoListService.GetTodoListByIdAsync(request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>Gets all to-do lists.</summary>
        [HttpGet("GetAllTodoLists")]
        [ProducesResponseType(typeof(TodoListsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoListsResponse>> GetAllTodoLists()
        {
            var result = await _todoListService.GetAllTodoListsAsync();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>Adds a new to-do list.</summary>
        [HttpPost("AddTodoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddTodoList([FromBody] AddTodoListRequest list)
        {
            await _todoListService.AddTodoListAsync(list);
            return Ok();
        }

        /// <summary>Updates an existing to-do list.</summary>
        [HttpPut("UpdateTodoList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTodoList([FromBody] UpdateTodoListRequest list)
        {
            await _todoListService.UpdateTodoListAsync(list);
            return Ok();
        }

        /// <summary>Deletes a to-do list by ID.</summary>
        /// <remarks>Route kept as GET for test compatibility.</remarks>
        [HttpDelete("DeleteTodoList/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTodoList([FromRoute] IdRequest request)
        {
            await _todoListService.DeleteTodoListAsync(request);
            return Ok();
        }
    }
}