using AutoMapper;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;

namespace TaskManager.Application.UseCases.Users
{
    public class UpdateUser(IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IPasswordHasher passwordHasher) : IUseCase
    {
        public record Request(string? UserName, string? Email, string? Password);
        public record Response(string Token);

        public async Task<Response> Handle(Guid userId, Request request)
        {
            var user = await unitOfWork.UserRepository.GetById(userId);

            if (request.UserName is not null)
                user!.UserName = request.UserName;

            if (request.Email is not null)
                user!.Email = request.Email;

            if (request.Password is not null)
                user!.PasswordHash = passwordHasher.Hash(request.Password);

            unitOfWork.UserRepository.Update(user!);
            await unitOfWork.Commit();

            var jwtToken = jwtProvider.GenerateToken(user!);
            return new Response(jwtToken);
        }
    }
}