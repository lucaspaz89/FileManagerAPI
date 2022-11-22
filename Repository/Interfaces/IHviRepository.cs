using FileManagerAPI.Models;

namespace FileManagerAPI.Repository.Interfaces
{
    public interface IHviRepository
    {
        Task<string> UploadHVI(List<HVI> model);
    }
}
