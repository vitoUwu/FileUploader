using FileUploader.Application.UseCases.Users.UploadProfilePhoto;
using FileUploader.Domain.Storage;
using FileUploader.Infrastructure.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IUploadProfilePhotoUseCase, UploadProfilePhotoUseCase>();
builder.Services.AddScoped<IStorageService>(options =>
{
    var clientId = builder.Configuration.GetValue<string>("CloudStorage:ClientId");
    var clientSecret = builder.Configuration.GetValue<string>("CloudStorage:ClientSecret");
    var applicationName = "FileUploader";
    var initializer = new GoogleAuthorizationCodeFlow.Initializer
    {
        ClientSecrets = new ClientSecrets
        {
            ClientId = clientId,
            ClientSecret = clientSecret
        },
        Scopes = [Google.Apis.Drive.v3.DriveService.Scope.Drive],
        DataStore = new FileDataStore(applicationName)
    };

    var flow = new GoogleAuthorizationCodeFlow(initializer);

    return new GoogleDriveStorageService(flow, applicationName);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
