using FileManagerAPI.Models;

namespace FileManagerAPI.Repository
{
    public interface ILoginRepository
    {
        Task<int> Login(UserLogin user);
    }
}
