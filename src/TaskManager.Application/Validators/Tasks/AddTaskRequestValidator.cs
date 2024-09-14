using FluentValidation;
using TaskManager.Application.Dtos.Tasks;

namespace TaskManager.Application.Validators.Tasks
{
    internal class AddTaskRequestValidator : AbstractValidator<AddTaskRequest>
    {
        public AddTaskRequestValidator()
        {
            RuleFor(task => task.Header).NotNull().Length(1, 50);
            RuleFor(task => task.Header).MaximumLength(500);
        }
    }
}