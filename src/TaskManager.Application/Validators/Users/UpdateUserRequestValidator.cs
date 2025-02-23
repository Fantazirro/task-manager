using FluentValidation;
using TaskManager.Application.UseCases.Users;

namespace TaskManager.Application.Validators.Users
{
    internal class UpdateUserRequestValidator : AbstractValidator<UpdateUser.Request>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.UserName).Length(3, 30);
            RuleFor(u => u.Password).MinimumLength(6);
        }
    }
}