using AutoMapper;
using TaskManager.Application.Models.Tasks;
using TaskManager.Application.UseCases.Auth;
using TaskManager.Application.UseCases.Tasks;
using TaskManager.Application.UseCases.Users;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddTask.Request, TaskEntity>();
            CreateMap<UpdateTask.Request, TaskEntity>();
            CreateMap<TaskEntity, TaskResponse>();

            CreateMap<SignUp.Request, User>();

            CreateMap<User, GetUserById.Response>();
        }
    }
}