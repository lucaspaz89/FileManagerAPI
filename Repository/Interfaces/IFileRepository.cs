using FileManagerAPI.Models;

namespace FileManagerAPI.Repository.Interfaces
{
    public interface IFileRepository
    {
        Task<bool> UploadFile(Files file);
    }
}
