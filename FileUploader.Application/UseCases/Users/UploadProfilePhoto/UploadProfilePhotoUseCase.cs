using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using FileUploader.Domain.Entities;
using FileUploader.Domain.Storage;
using Microsoft.AspNetCore.Http;

namespace FileUploader.Application.UseCases.Users.UploadProfilePhoto
{
    public class UploadProfilePhotoUseCase : IUploadProfilePhotoUseCase
    {
        private readonly IStorageService _storageService;

        public UploadProfilePhotoUseCase(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public void Execute(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var isImage = IsImage(stream);

            if (!isImage)
            {
                throw new Exception("The file is not an JPEG");
            }

            _storageService.Upload(file, GetUser());
        }

        private static bool IsImage(Stream stream)
        {
            return stream.Is<JointPhotographicExpertsGroup>() || stream.Is<PortableNetworkGraphic>();
        }

        private static User GetUser()
        {
            return new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@gmail.com",
                AccessToken = "your access token",
                RefreshToken = "your refresh token"
            };
        }
    }
}
