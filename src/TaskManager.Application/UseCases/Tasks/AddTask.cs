using AutoMapper;
using TaskManager.Application.Models.Tasks;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.UseCases.Tasks
{
    public class AddTask(IUnitOfWork unitOfWork, IMapper mapper) : IUseCase
    {
        public record Request(string Header, string Description, DateTimeOffset? Deadline);

        public async Task<TaskResponse> Handle(Guid userId, Request request)
        {
            var task = mapper.Map<TaskEntity>(request);
            task.UserId = userId;

            await unitOfWork.TaskRepository.Add(task);
            await unitOfWork.Commit();

            return mapper.Map<TaskResponse>(task);
        }
    }
}