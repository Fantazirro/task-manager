using FluentValidation;
using TaskManager.Application.Dtos.User;

namespace TaskManager.Application.Validators.User
{
    public class UserSignInDtoValidator : AbstractValidator<UserSignInDto>
    {
        public UserSignInDtoValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}