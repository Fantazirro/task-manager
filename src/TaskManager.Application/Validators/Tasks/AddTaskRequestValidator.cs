using FluentValidation;
using TaskManager.Application.UseCases.Tasks;

namespace TaskManager.Application.Validators.Tasks
{
    internal class AddTaskRequestValidator : AbstractValidator<AddTask.Request>
    {
        public AddTaskRequestValidator()
        {
            RuleFor(task => task.Header).NotNull().Length(1, 50);
            RuleFor(task => task.Description).MaximumLength(2000);
        }
    }
}