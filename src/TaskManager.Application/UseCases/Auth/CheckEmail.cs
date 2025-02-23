using TaskManager.Application.Interfaces.Common;
using TaskManager.Application.Interfaces.Data;

namespace TaskManager.Application.UseCases.Auth
{
    public class CheckEmail(IUnitOfWork unitOfWork) : IUseCase
    {
        public async Task<bool> Handle(string email)
        {
            var user = await unitOfWork.UserRepository.GetByEmail(email);
            return user != null;
        }
    }
}