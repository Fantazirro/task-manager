using AutoMapper;
using TaskManager.Application.Dtos.UserTask;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Services
{
    public class UserTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserTask?> GetById(Guid userId, Guid taskId)
        {
            var task = await _unitOfWork.UserTaskRepository.GetById(taskId);
            if (task is not null && task.UserId != userId) throw new NotFoundException(userId, taskId, typeof(UserTask));
            return task;
        }

        public async Task<IEnumerable<UserTask>> GetByUserId(Guid userId)
        {
            return await _unitOfWork.UserTaskRepository.GetByUserId(userId);
        }

        public async Task Add(Guid userId, AddUserTaskDto addTask)
        {
            var task = _mapper.Map<UserTask>(addTask);
            task.UserId = userId;

            await _unitOfWork.UserTaskRepository.Add(task);
            await _unitOfWork.Commit();
        }

        public async Task Update(Guid userId, UpdateUserTaskDto updateTask)
        {
            var task = await _unitOfWork.UserTaskRepository.GetById(updateTask.Id);
            if (task is null || task.UserId != userId) throw new NotFoundException(userId, updateTask.Id, typeof(UserTask));

            _mapper.Map(updateTask, task);

            _unitOfWork.UserTaskRepository.Update(task);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid userId, Guid taskId)
        {
            var task = await _unitOfWork.UserTaskRepository.GetById(taskId);
            if (task is null || task.UserId != userId) throw new NotFoundException(userId, taskId, typeof(UserTask));

            _unitOfWork.UserTaskRepository.Delete(task);
            await _unitOfWork.Commit();
        }
    }
}