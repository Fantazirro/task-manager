using AutoMapper;
using TaskManager.Application.Models.Tasks;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.UseCases.Tasks
{
    public class GetTaskById(IUnitOfWork unitOfWork, IMapper mapper) : IUseCase
    {
        public async Task<TaskResponse?> Handle(Guid userId, Guid taskId)
        {
            var task = await unitOfWork.TaskRepository.GetById(taskId);
            if (task is not null && task.UserId != userId) throw new NotFoundException(userId, taskId, typeof(TaskEntity));
            return mapper.Map<TaskResponse>(task);
        }
    }
}