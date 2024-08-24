using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos.UserTask;
using TaskManager.Application.Services;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]  
    public class TasksController : ControllerBase
    {
        private readonly UserTaskService _userTaskService;
        private readonly Guid _userId;

        public TasksController(UserTaskService userTaskService, IHttpContextAccessor httpContextAccessor)
        {
            _userTaskService = userTaskService;

            var httpContext = httpContextAccessor.HttpContext;
            _userId = Guid.Parse(httpContext!.User.FindFirst("user_id")!.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _userTaskService.GetById(_userId, id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId()
        {
            var id = _userId;
            var tasks = await _userTaskService.GetByUserId(id);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserTaskDto task, [FromServices] IValidator<AddUserTaskDto> validator)
        {
            var validationResult = validator.Validate(task);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            await _userTaskService.Add(_userId, task);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserTaskDto task, [FromServices] IValidator<UpdateUserTaskDto> validator)
        {
            var validationResult = validator.Validate(task);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            await _userTaskService.Update(_userId, task);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userTaskService.Delete(_userId, id);
            return Ok();
        }
    }
}