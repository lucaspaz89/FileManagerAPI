using FileManagerAPI.Models;

namespace FileManagerAPI.Repository
{
    public interface IFileRepository
    {
        Task<IFormFile> UploadFile(Files file);
    }
}
