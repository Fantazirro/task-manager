using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos.Tasks;
using TaskManager.Application.Services;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]  
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly Guid _userId;

        public TasksController(TaskService taskService, IHttpContextAccessor httpContextAccessor)
        {
            _taskService = taskService;

            var httpContext = httpContextAccessor.HttpContext;
            _userId = Guid.Parse(httpContext!.User.FindFirst("user_id")!.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _taskService.GetById(_userId, id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            var id = _userId;
            var tasks = await _taskService.GetByUserId(id);
            return Ok(tasks);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryByUserId()
        {
            var id = _userId;
            var tasks = await _taskService.GetHistoryByUserId(id);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTaskRequest addTaskRequest, [FromServices] IValidator<AddTaskRequest> validator)
        {
            var validationResult = validator.Validate(addTaskRequest);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var task = await _taskService.Add(_userId, addTaskRequest);
            return Ok(task);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskRequest updateTaskRequest, [FromServices] IValidator<UpdateTaskRequest> validator)
        {
            var validationResult = validator.Validate(updateTaskRequest);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var task = await _taskService.Update(_userId, updateTaskRequest);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _taskService.Delete(_userId, id);
            return Ok();
        }
    }
}