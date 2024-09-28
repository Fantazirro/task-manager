using FluentValidation;
using TaskManager.Application.UseCases.Auth;

namespace TaskManager.Application.Validators.Auth
{
    public class SignUpRequestValidator : AbstractValidator<SignUp.Request>
    {
        public SignUpRequestValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.UserName).Length(3, 30);
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}