using FileUploader.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FileUploader.Domain.Storage
{
    public interface IStorageService
    {
        string Upload(IFormFile file, User user);
    }
}
