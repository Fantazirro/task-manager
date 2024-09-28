using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Users;
using TaskManager.Infrastructure.Authentication;

namespace TaskManager.Api.Controllers
{
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
    }
}