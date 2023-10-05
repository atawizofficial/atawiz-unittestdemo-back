using Atawiz.UnitTestDemo.Core.Dtos;
using Atawiz.UnitTestDemo.Core.Exceptions;
using Atawiz.UnitTestDemo.Core.Models;
using Atawiz.UnitTestDemo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atawiz.UnitTestDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        // GET: api/TodoItem
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TodoItemDto> result = await _todoItemService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/TodoItem/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                TodoItemDto result = await _todoItemService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (NotFoundException<TodoItem> ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/TodoItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoItemDto todoItemDto)
        {
            try
            {
                TodoItemDto result = await _todoItemService.CreateAsync(todoItemDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is AlreadyExistingException<TodoItem>)
                    return StatusCode(StatusCodes.Status409Conflict, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/TodoItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TodoItemDto todoItemDto)
        {
            try
            {
                TodoItemDto result = await _todoItemService.UpdateAsync(id, todoItemDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException<TodoItem>)
                    return NotFound(ex.Message);
                if (ex is AlreadyExistingException<TodoItem>)
                    return StatusCode(StatusCodes.Status409Conflict, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int result = await _todoItemService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException<TodoItem>)
                    return NotFound(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
