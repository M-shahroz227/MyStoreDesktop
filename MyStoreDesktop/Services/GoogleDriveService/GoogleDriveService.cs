using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

namespace POSApp
{
    public static class GoogleDriveServiceHelper
    {
        private static DriveService _service;

        // ✅ FULL SCOPE (access all Google Drive files)
        private static readonly string[] Scopes =
        {
            DriveService.Scope.Drive
        };

        public static DriveService GetService(string userEmail)
        {
            if (_service != null)
                return _service;

            // ✅ Use absolute path for credentials.json
            string credentialPath = @"F:\SmartStore\EcommerceDDD\MyDesktopStore\MyStoreDesktop\Credentials\credentials.json";

            if (!File.Exists(credentialPath))
                throw new FileNotFoundException(
                    "credentials.json not found. Please place it in the specified path.",
                    credentialPath
                );

            UserCredential credential;

            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                // ✅ Token stored per Gmail user
                string tokenPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MyStoreDesktop",
                    "GoogleTokens",
                    userEmail.Replace("@", "_").Replace(".", "_")
                );

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    userEmail,                   // 👈 Test User Gmail
                    CancellationToken.None,
                    new FileDataStore(tokenPath, true)
                ).Result;
            }

            _service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MyStoreDesktop"
            });

            return _service;
        }

        // ✅ CREATE / GET FOLDER
        public static string GetOrCreateFolder(DriveService service, string folderName)
        {
            var listRequest = service.Files.List();
            listRequest.Q =
                $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
            listRequest.Fields = "files(id, name)";

            var result = listRequest.Execute();

            if (result.Files != null && result.Files.Count > 0)
                return result.Files[0].Id;

            var folderMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            var createRequest = service.Files.Create(folderMetadata);
            createRequest.Fields = "id";

            return createRequest.Execute().Id;
        }
    }
}
