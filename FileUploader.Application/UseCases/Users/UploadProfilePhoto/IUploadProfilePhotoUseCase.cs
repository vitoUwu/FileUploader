using Microsoft.AspNetCore.Http;

namespace FileUploader.Application.UseCases.Users.UploadProfilePhoto
{
    public interface IUploadProfilePhotoUseCase
    {
       public void Execute(IFormFile file);
    }
}
