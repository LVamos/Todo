using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Todo.Contracts.Contracts.Requests;
using Todo.Contracts.Contracts.Responses;
using Todo.Services.Abstraction.Services;

namespace Todo.API.Controllers
{
    /// <summary>
    /// API endpoints for working with to-do items within lists.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>Gets all to-do items for a list.</summary>
        [HttpGet("GetTodoItemsByListId/{listId}")]
        [ProducesResponseType(typeof(TodoItemsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemsResponse>> GetTodoItemsByListId(int listId)
        {
            try
            {
                var request = new IdRequest { Id = listId };
                var result = await _todoItemService.GetTodoItemsByListIdAsync(request);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>Gets a to-do item by ID.</summary>
        [HttpGet("GetTodoItemById/{id}")]
        [ProducesResponseType(typeof(TodoItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemResponse>> GetTodoItemById(int id)
        {
            try
            {
                var request = new IdRequest { Id = id };
                var result = await _todoItemService.GetTodoItemByIdAsync(request);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>Adds a new to-do item.</summary>
        [HttpPost("AddTodoItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddTodoItem([FromBody] AddTodoItemRequest item)
        {
            try
            {
                await _todoItemService.AddTodoItemAsync(item);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>Updates an existing to-do item.</summary>
        [HttpPut("UpdateTodoItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateTodoItem([FromBody] UpdateTodoItemRequest item)
        {
            try
            {
                await _todoItemService.UpdateTodoItemAsync(item);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary>Deletes a to-do item by ID.</summary>
        /// <remarks>Note: Uses GET route for deletion to keep compatibility with existing client/tests.</remarks>
        [HttpGet("DeleteTodoItem/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTodoItem(int id)
        {
            try
            {
                var request = new IdRequest { Id = id };
                await _todoItemService.DeleteTodoItemAsync(request);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        private ActionResult ServerError(Exception ex) =>
            StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Error = ex.Message });
    }
}