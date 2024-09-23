using FluentValidation;
using TaskManager.Application.Dtos.Tasks;

namespace TaskManager.Application.Validators.Tasks
{
    internal class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(task => task.Header).NotNull().Length(1, 50);
            RuleFor(task => task.Description).MaximumLength(2000);
        }
    }
}