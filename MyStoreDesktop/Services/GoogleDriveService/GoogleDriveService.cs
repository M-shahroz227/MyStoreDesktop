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

        // ✅ Full access to Google Drive
        private static readonly string[] Scopes = { DriveService.Scope.Drive };

        public static DriveService GetService(string userEmail)
        {
            if (_service != null)
                return _service;

            SettingService settings = new SettingService();

            // ✅ Professional: use SettingService
            string credentialPath = settings.GetGoogleCredentialPath();

            if (!File.Exists(credentialPath))
                throw new FileNotFoundException("Google credentials file not found.", credentialPath);

            UserCredential credential;

            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                string tokenPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    settings.GetGoogleTokenFolder(),
                    SanitizeEmail(userEmail)
                );

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    userEmail,
                    CancellationToken.None,
                    new FileDataStore(tokenPath, true)
                ).Result;
            }

            _service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = settings.GetAppName()
            });

            return _service;
        }

        // 🔧 Email safe for folder names
        private static string SanitizeEmail(string email) => email.Replace("@", "_").Replace(".", "_");

        // 📁 Create or get folder
        public static string GetOrCreateFolder(DriveService service, string folderName)
        {
            var listRequest = service.Files.List();
            listRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
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
