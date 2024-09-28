using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Tasks;
using TaskManager.Infrastructure.Authentication;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]  
    public class TasksController : ControllerBase
    {
        private readonly Guid _userId;

        public TasksController(UserIdProvider userIdProvider)
        {
            _userId = (Guid)userIdProvider.GetUserId()!;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, GetTaskById useCase)
        {
            var task = await useCase.Handle(_userId, id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(GetTasksByUserId useCase)
        {
            var tasks = await useCase.Handle(new(_userId));
            return Ok(tasks);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryByUserId(GetTaskHistoryByUserId useCase)
        {
            var tasks = await useCase.Handle(_userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTask.Request request, AddTask useCase, IValidator<AddTask.Request> validator)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var task = await useCase.Handle(_userId, request);
            return Ok(task);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTask.Request request, UpdateTask useCase, IValidator<UpdateTask.Request> validator)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var task = await useCase.Handle(_userId, request);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, DeleteTask useCase)
        {
            await useCase.Handle(_userId, id);
            return Ok();
        }
    }
}