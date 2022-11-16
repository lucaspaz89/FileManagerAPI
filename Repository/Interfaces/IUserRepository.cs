using FileManagerAPI.Models;

namespace FileManagerAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(UserRegistration user);
    }
}
