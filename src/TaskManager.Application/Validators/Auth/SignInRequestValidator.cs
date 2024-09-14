using FluentValidation;
using TaskManager.Application.Dtos.Auth;

namespace TaskManager.Application.Validators.Auth
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}