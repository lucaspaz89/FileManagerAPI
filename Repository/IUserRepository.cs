using FileManagerAPI.Models;

namespace FileManagerAPI.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(UserRegistration user);
    }
}
