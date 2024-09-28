using AutoMapper;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.UseCases.Auth
{
    public class SignUp(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) : IUseCase
    {
        public record Request(string UserName, string Email, string Password);
        public record Response(string Token);

        public async Task<Response> Handle(Request request)
        {
            var user = await unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user is not null) throw new BadRequestException($"User with email {request.Email} already exists");

            user = mapper.Map<User>(request);
            user.PasswordHash = passwordHasher.Hash(request.Password);

            await unitOfWork.UserRepository.Add(user);
            await unitOfWork.Commit();

            return new(jwtProvider.GenerateToken(user));
        }
    }
}