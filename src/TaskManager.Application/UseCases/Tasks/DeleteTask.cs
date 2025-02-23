using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.UseCases.Tasks
{
    public class DeleteTask(IUnitOfWork unitOfWork) : IUseCase
    {
        public async Task Handle(Guid userId, Guid taskId)
        {
            var task = await unitOfWork.TaskRepository.GetById(taskId);
            if (task is null || task.UserId != userId) throw new NotFoundException(userId, taskId, typeof(TaskEntity));

            unitOfWork.TaskRepository.Delete(task);
            await unitOfWork.Commit();
        }
    }
}