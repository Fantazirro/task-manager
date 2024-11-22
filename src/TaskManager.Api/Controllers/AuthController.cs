using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Filters;
using TaskManager.Application.UseCases.Auth;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("sign-in")]
        [ValidationFilter<SignIn.Request>]
        public async Task<IActionResult> SignIn([FromBody] SignIn.Request request, SignIn useCase)
        {
            var signInResponse = await useCase.Handle(request);
            return Ok(signInResponse);
        }

        [HttpPost("sign-up")]
        [ValidationFilter<SignUp.Request>]
        public async Task<IActionResult> SignUp(
            [FromBody] SignUp.Request request,
            [FromQuery] int code,
            SignUp signUpUseCase,
            ConfirmEmail confirmEmailUseCase)
        {
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


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, ResetPassword useCase)
        {
            var success = await useCase.Handle(email);
            return success ? Ok() : BadRequest("Пользователя с данной почтой не существует");
        }

        [HttpPost("confirm-reset")]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPassword.Request request, ConfirmResetPassword useCase)
        {
            // сделать валидацию для пароля
            var success = await useCase.Handle(request);
            return success ? Ok() : BadRequest("Токен недействителен");
        }
    }
}