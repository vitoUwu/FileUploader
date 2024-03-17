<h1 align="center" style="font-weight: bold;">File Uploader</h1>

<p align="center">
    <b>A file uploader API made with ASP.NET</b>
</p>

<h2>Getting started</h2>

<h3>Prerequisites</h3>

- [.NET](https://dotnet.microsoft.com/)
- [Git](https://git-scm.com/)


<h3>Cloning</h3>

```bash
git clone https://github.com/vitoUwu/FileUploader
```

<h3>Config enviroment variables</h2>

Configure the `FileUploader/FileUploader.API/appsettings.Development.json` with your Google API Credentials

[Don't have a Google Cloud Account?](https://developers.google.com/workspace/guides/get-started)

```json
"CloudStorage": {
    "ClientId": "your client id",
    "ClientSecret": "your client secret"
}
```

Also, don't forget to configure the `FileUploader/FileUploader.Application/UseCases/Users/UploadProfilePhoto/UploadProfilePhotoUseCase.cs`

```cs
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
```

You can get these credentials [here](https://developers.google.com/oauthplayground/)

<h3>Starting</h3>

```bash
cd FileUploader
dotnet run --project FileUploader.API
```
