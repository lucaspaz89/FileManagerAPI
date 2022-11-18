using FileManagerAPI.Models;

namespace FileManagerAPI.Repository.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> Login(UserLogin user);
        Task<int> GetById(int id);
    }
}
