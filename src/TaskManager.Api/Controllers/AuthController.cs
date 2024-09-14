using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Services;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _userService;

        public AuthController(AuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signInRequest, IValidator<SignInRequest> validator)
        {
            var validationResult = validator.Validate(signInRequest);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var signInViewModel = await _userService.SignIn(signInRequest);
            return Ok(signInViewModel);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest, IValidator<SignUpRequest> validator)
        {
            var validationResult = validator.Validate(signUpRequest);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            await _userService.SignUp(signUpRequest);
            return Ok();
        }
    }
}