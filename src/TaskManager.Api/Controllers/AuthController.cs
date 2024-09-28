using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.Auth;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn.Request request, SignIn useCase, IValidator<SignIn.Request> validator)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var signInResponse = await useCase.Handle(request);
            return Ok(signInResponse);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(
            [FromBody] SignUp.Request request,
            [FromQuery] int code,
            SignUp signUpUseCase,
            ConfirmEmail confirmEmailUseCase,
            IValidator<SignUp.Request> validator)
        {
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var success = await confirmEmailUseCase.Handle(new(request.Email, code));
            if (!success) return BadRequest("Неверный код");

            var signUpResponse = await signUpUseCase.Handle(request);
            return Ok(signUpResponse);
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromQuery] string email, SendCode useCase)
        {
            await useCase.Handle(email);
            return Ok();
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email, CheckEmail useCase)
        {
            var isEmailTaken = await useCase.Handle(email);
            return Ok(isEmailTaken);
        }
    }
}