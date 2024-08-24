using FluentValidation;
using TaskManager.Application.Dtos.User;

namespace TaskManager.Application.Validators.User
{
    public class UserSignUpDtoValidator : AbstractValidator<UserSignUpDto>
    {
        public UserSignUpDtoValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.UserName).Length(3, 30);
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}