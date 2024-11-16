using AutoMapper;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.UseCases.Users
{
    public class UpdateUser(IUnitOfWork unitOfWork, IMapper mapper, IJwtProvider jwtProvider) : IUseCase
    {
        public record Request(string UserName, string Email);
        public record Response(string Token);

        public async Task<Response> Handle(Guid userId, Request request)
        {
            var user = await unitOfWork.UserRepository.GetById(userId);
            user = mapper.Map(request, user, request.GetType(), user!.GetType()) as User;
            unitOfWork.UserRepository.Update(user!);
            await unitOfWork.Commit();

            var jwtToken = jwtProvider.GenerateToken(user!);
            return new Response(jwtToken);
        }
    }
}