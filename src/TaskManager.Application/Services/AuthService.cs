using AutoMapper;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<SignInResponse> SignIn(SignInRequest signInRequest)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(signInRequest.Email);
            if (user is null) throw new BadRequestException($"User with email {signInRequest.Email} doesn't exists");

            if (!_passwordHasher.Verify(signInRequest.Password, user.PasswordHash)) throw new BadRequestException("Incorrect password");

            return new SignInResponse() { Token = _jwtProvider.GenerateToken(user) };
        }

        public async Task SignUp(SignUpRequest signUpRequest)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(signUpRequest.Email);
            if (user is not null) throw new BadRequestException($"User with email {signUpRequest.Email} already exists");

            user = _mapper.Map<User>(signUpRequest);
            user.PasswordHash = _passwordHasher.Hash(signUpRequest.Password);

            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Commit();
        }
    }
}