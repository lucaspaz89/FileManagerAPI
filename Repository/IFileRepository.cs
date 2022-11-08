using FileManagerAPI.Models;

namespace FileManagerAPI.Repository
{
    public interface IFileRepository
    {
        Task<bool> UploadFile(Files file);
    }
}
