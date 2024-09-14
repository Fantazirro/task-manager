using FluentValidation;
using TaskManager.Application.Dtos.Auth;

namespace TaskManager.Application.Validators.Auth
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.UserName).Length(3, 30);
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}