using AutoMapper;
using TaskManager.Application.Models.Tasks;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.UseCases.Tasks
{
    public class UpdateTask(IUnitOfWork unitOfWork, IMapper mapper) : IUseCase
    {
        public record Request(
            Guid Id, 
            string Header, 
            string Description, 
            TaskEntityStatus Status,
            DateTimeOffset? Deadline);

        public async Task<TaskResponse> Handle(Guid userId, Request request)
        {
            var task = await unitOfWork.TaskRepository.GetById(request.Id);
            if (task is null || task.UserId != userId) throw new NotFoundException(userId, request.Id, typeof(TaskEntity));

            if (task.Status == TaskEntityStatus.InProgress && request.Status == TaskEntityStatus.Completed)
            {
                task.CompletedDate = DateTimeOffset.UtcNow;
            }
            else if (task.Status == TaskEntityStatus.Completed && request.Status == TaskEntityStatus.InProgress)
            {
                task.CompletedDate = null;
            }

            mapper.Map(request, task);

            unitOfWork.TaskRepository.Update(task);
            await unitOfWork.Commit();

            return mapper.Map<TaskResponse>(task);
        }
    }
}