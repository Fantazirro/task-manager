using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.UseCases.Auth
{
    public class SignIn (IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) : IUseCase
    {
        public record Request(string Email, string Password);
        public record Response(string Token);

        public async Task<Response> Handle(Request request)
        {
            var user = await unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user is null) throw new BadRequestException($"User with email {request.Email} doesn't exists");

            if (!passwordHasher.Verify(request.Password, user.PasswordHash)) throw new BadRequestException("Incorrect password");

            return new Response(jwtProvider.GenerateToken(user));
        }
    }
}