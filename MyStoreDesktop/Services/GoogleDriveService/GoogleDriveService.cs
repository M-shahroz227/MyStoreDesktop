using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace POSApp
{
    public static class GoogleDriveServiceHelper
    {
        private static DriveService _service;

        // 🔒 DriveFile Scope (app-only access, safe)
        private static readonly string[] Scopes =
        {
            DriveService.Scope.DriveFile
        };

        public static DriveService GetService(string userEmail)
        {
            if (_service != null)
                return _service;

            // ✅ Relative path to credentials.json
            string credentialPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Credentials",
                "credentials.json"
            );

            if (!File.Exists(credentialPath))
                throw new FileNotFoundException(
                    "credentials.json missing. Place it in the Credentials folder.",
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
                ApplicationName = "MyStoreDesktop"
            });

            return _service;
        }

        private static string SanitizeEmail(string email)
        {
            return email.Replace("@", "_").Replace(".", "_");
        }

        // 📁 Create / Get Folder in Google Drive
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
