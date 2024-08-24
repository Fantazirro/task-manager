using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos.User;
using TaskManager.Application.Services;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInDto userSignInDto, IValidator<UserSignInDto> validator)
        {
            var validationResult = validator.Validate(userSignInDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var jwtToken = await _userService.SignIn(userSignInDto);
            return Ok(jwtToken);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDto userSignUpDto, IValidator<UserSignUpDto> validator)
        {
            var validationResult = validator.Validate(userSignUpDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            await _userService.SignUp(userSignUpDto);
            return Ok();
        }
    }
}