using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serdiuk.ToDoList.API.Controllers.Base;
using Serdiuk.ToDoList.Application.Common.Interfaces;
using Serdiuk.ToDoList.Application.Dtos.ToDo;
using Serdiuk.ToDoList.Application.Exceptions;

namespace Serdiuk.ToDoList.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/todo")]
    public class TodoController : BaseApiController
    {
        private readonly IToDoItemService _todoService;
        private readonly ILogger<TodoController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(IToDoItemService todoService,
            ILogger<TodoController> logger,
            UserManager<IdentityUser> userManager)
        {
            _todoService = todoService;
            _logger = logger;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (UserId == null)
            {
                throw new TodoUnathorizeException();
            } 

            var list = await _todoService.GetAll(UserId);

            _logger.LogInformation("Returned all todos to: {Email}", UserId);

            return Ok(list);
        }

        [HttpGet("complete")]
        public async Task<IActionResult> GetComplete()
        {

            var list = await _todoService.GetCompleteToDoAsync(UserId);
            _logger.LogInformation("Returned completed todos to: {Email}", UserId);
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var todo = await _todoService.GetById(id, UserId);
            _logger.LogInformation("Returned todo to: {Email} with {id} Id", UserId, UserId);
            return Ok(todo);
        }
        [HttpGet("incomplete")]
        public async Task<IActionResult> GetIncomplete()
        {

            var list = await _todoService.GetIncompleteToDoAsync(UserId);
            _logger.LogInformation("Returned incompleted todos to: {Email}", UserId);
            return Ok(list);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateToDoItemDto dto)
        {
            if (UserId == null)
            {
                throw new TodoUnathorizeException();
            }
            var id = await _todoService.AddToDoAsync(dto, UserId);
            _logger.LogInformation("Create new todo to: {Email} with {id} Id", UserId, id);
            return CreatedAtAction(Url.Action(nameof(GetById)), new { Id = id });
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteToDoItemDto dto)
        {

            var id = await _todoService.DeleteToDoAsync(dto, UserId);

            _logger.LogInformation("Delete todo with {id} Id from user {Email}", id, UserId);
            return NoContent();
        }
        [HttpPut("done")]
        public async Task<IActionResult> UpdateDone([FromBody] UpdateDoneToDoItemDto dto)
        {
            await _todoService.UpdateDoneAsync(dto, UserId);
            return NoContent();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateTodo([FromBody] UpdateToDoItemDto dto)
        {
            await _todoService.UpdateToDoAsync(dto, UserId);
            return NoContent();
        }
    }
}
