using AutoMapper;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Dtos.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddTaskRequest, TaskEntity>();
            CreateMap<UpdateTaskRequest, TaskEntity>();
            CreateMap<TaskEntity, TaskResponse>();

            CreateMap<SignUpRequest, User>();
        }
    }
}