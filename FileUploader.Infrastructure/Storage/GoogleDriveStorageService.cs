using FileUploader.Domain.Entities;
using FileUploader.Domain.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Http;

namespace FileUploader.Infrastructure.Storage
{
    public class GoogleDriveStorageService : IStorageService
    {
        private readonly GoogleAuthorizationCodeFlow _flow;
        private readonly string _applicationName;

        public GoogleDriveStorageService(GoogleAuthorizationCodeFlow flow, string applicationName)
        {
            _flow = flow;
            _applicationName = applicationName;
        }

        public string Upload(IFormFile file, User user)
        {
            var credential = new UserCredential(_flow, user.Email, new Google.Apis.Auth.OAuth2.Responses.TokenResponse
            {
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken
            });
            var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer
            {
                ApplicationName = _applicationName,
                HttpClientInitializer = credential
            });

            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.Name,
                MimeType = file.ContentType,
            };

            var command = service.Files.Create(driveFile, file.OpenReadStream(), file.ContentType);
            command.Fields = "id";

            var response = command.Upload();

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            return command.ResponseBody.Id;
        }
    }
}
