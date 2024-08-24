using FluentValidation;
using TaskManager.Application.Dtos.UserTask;

namespace TaskManager.Application.Validators.UserTask
{
    internal class UpdateUserTaskDtoValidator : AbstractValidator<UpdateUserTaskDto>
    {
        public UpdateUserTaskDtoValidator()
        {
            RuleFor(task => task.Header).NotNull().Length(1, 50);
            RuleFor(task => task.Header).MaximumLength(500);
        }
    }
}