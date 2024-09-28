using AutoMapper;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;

namespace TaskManager.Application.UseCases.Users
{
    public class GetUserById(IUnitOfWork unitOfWork, IMapper mapper) : IUseCase
    {
        public record Response(string Id, string UserName, string Email);
        
        public async Task<Response?> Handle(Guid userId)
        {
            var user = await unitOfWork.UserRepository.GetById(userId);
            return user is null ? null : mapper.Map<Response>(user);
        }
    }
}