using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Filters;
using TaskManager.Application.UseCases.Users;
using TaskManager.Infrastructure.Authentication;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Guid _userId;

        public UsersController(UserIdProvider userIdProvider)
        {
            _userId = (Guid)userIdProvider.GetUserId()! ;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(GetUserById useCase)
        {
            var user = await useCase.Handle(_userId);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPut]
        [ValidationFilter<UpdateUser.Request>]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser.Request request, UpdateUser useCase)
        {
            var response = await useCase.Handle(_userId, request);
            return Ok(response);
        }
    }
}