using FluentValidation;
using TaskManager.Application.UseCases.Auth;

namespace TaskManager.Application.Validators.Auth
{
    public class SignInRequestValidator : AbstractValidator<SignIn.Request>
    {
        public SignInRequestValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}