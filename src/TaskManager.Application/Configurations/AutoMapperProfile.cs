using AutoMapper;
using TaskManager.Application.Dtos.User;
using TaskManager.Application.Dtos.UserTask;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUserTaskDto, UserTask>();
            CreateMap<UpdateUserTaskDto, UserTask>();

            CreateMap<UserSignUpDto, User>();
        }
    }
}