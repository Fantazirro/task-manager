using AutoMapper;
using TaskManager.Application.Models.Tasks;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;

namespace TaskManager.Application.UseCases.Tasks
{
    public class GetTaskHistoryByUserId(IUnitOfWork unitOfWork, IMapper mapper) : IUseCase
    {
        public async Task<IEnumerable<TaskResponse>> Handle(Guid userId)
        {
            var tasks = await unitOfWork.TaskRepository.GetHistoryByUserId(userId);
            return tasks.Select(mapper.Map<TaskResponse>);
        }
    }
}