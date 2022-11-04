using FileManagerAPI.Models;

namespace FileManagerAPI.Repository
{
    public interface IFileRepository
    {
        Task<string> UploadFile(Files file);
    }
}
