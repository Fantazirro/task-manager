using AutoMapper;
using TaskManager.Application.Dtos.User;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Data;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> SignIn(UserSignInDto userSignInDto)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(userSignInDto.Email);
            if (user is null) throw new BadRequestException($"User with email {userSignInDto.Email} doesn't exists");

            var passwordHash = _passwordHasher.Hash(userSignInDto.Password);
            if (_passwordHasher.Verify(passwordHash, user.PasswordHash)) throw new BadRequestException("Incorrect password");

            return _jwtProvider.GenerateToken(user);
        }

        public async Task SignUp(UserSignUpDto userSignUpDto)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(userSignUpDto.Email);
            if (user is not null) throw new BadRequestException($"User with email {userSignUpDto.Email} already exists");

            user = _mapper.Map<User>(userSignUpDto);
            user.PasswordHash = _passwordHasher.Hash(userSignUpDto.Password);

            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Commit();
        }
    }
}