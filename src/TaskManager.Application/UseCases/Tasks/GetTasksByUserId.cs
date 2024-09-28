using AutoMapper;
using TaskManager.Application.Models.Tasks;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;

namespace TaskManager.Application.UseCases.Tasks
{
    public class GetTasksByUserId(IUnitOfWork unitOfWork, IMapper mapper) : IUseCase
    {
        public record Request(Guid UserId);

        public async Task<IEnumerable<TaskResponse>> Handle(Request request)
        {
            var tasks = await unitOfWork.TaskRepository.GetByUserId(request.UserId);
            return tasks.Select(mapper.Map<TaskResponse>);
        }
    }
}