using FluentValidation;
using TaskManager.Application.Dtos.UserTask;

namespace TaskManager.Application.Validators.UserTask
{
    internal class AddUserTaskDtoValidator : AbstractValidator<AddUserTaskDto>
    {
        public AddUserTaskDtoValidator()
        {
            RuleFor(task => task.Header).NotNull().Length(1, 50);
            RuleFor(task => task.Header).MaximumLength(500);
        }
    }
}