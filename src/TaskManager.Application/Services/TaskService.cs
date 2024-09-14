using AutoMapper;
using TaskManager.Application.Dtos.Tasks;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Services
{
    public class TaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskResponse?> GetById(Guid userId, Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetById(taskId);
            if (task is not null && task.UserId != userId) throw new NotFoundException(userId, taskId, typeof(TaskEntity));
            return _mapper.Map<TaskResponse>(task);
        }

        public async Task<IEnumerable<TaskResponse>> GetByUserId(Guid userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetByUserId(userId);
            return tasks.Select(_mapper.Map<TaskResponse>);
        }

        public async Task<IEnumerable<TaskResponse>> GetHistoryByUserId(Guid userId)
        {
            var tasks = await _unitOfWork.TaskRepository.GetHistoryByUserId(userId);
            return tasks.Select(_mapper.Map<TaskResponse>);
        }

        public async Task<TaskResponse> Add(Guid userId, AddTaskRequest addTaskRequest)
        {
            var task = _mapper.Map<TaskEntity>(addTaskRequest);
            task.UserId = userId;

            await _unitOfWork.TaskRepository.Add(task);
            await _unitOfWork.Commit();

            return _mapper.Map<TaskResponse>(task);
        }

        public async Task<TaskResponse> Update(Guid userId, UpdateTaskRequest updateTaskRequest)
        {
            var task = await _unitOfWork.TaskRepository.GetById(updateTaskRequest.Id);
            if (task is null || task.UserId != userId) throw new NotFoundException(userId, updateTaskRequest.Id, typeof(TaskEntity));

            if (task.Status == TaskEntityStatus.InProgress && updateTaskRequest.Status == TaskEntityStatus.Completed)
            {
                task.CompletedDate = DateTimeOffset.UtcNow;
            }
            else if (task.Status == TaskEntityStatus.Completed && updateTaskRequest.Status == TaskEntityStatus.InProgress)
            {
                task.CompletedDate = null;
            }

            _mapper.Map(updateTaskRequest, task);

            _unitOfWork.TaskRepository.Update(task);
            await _unitOfWork.Commit();

            return _mapper.Map<TaskResponse>(task);
        }

        public async Task Delete(Guid userId, Guid taskId)
        {
            var task = await _unitOfWork.TaskRepository.GetById(taskId);
            if (task is null || task.UserId != userId) throw new NotFoundException(userId, taskId, typeof(TaskEntity));

            _unitOfWork.TaskRepository.Delete(task);
            await _unitOfWork.Commit();
        }
    }
}