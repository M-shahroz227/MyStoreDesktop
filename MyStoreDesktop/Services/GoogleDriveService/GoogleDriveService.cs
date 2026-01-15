using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MyStoreDesktop.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace POSApp
{
    public static class GoogleDriveServiceHelper
    {
        // Multi-user service storage
        private static readonly Dictionary<string, DriveService> Services = new Dictionary<string, DriveService>();


        private static readonly string[] Scopes = { DriveService.Scope.DriveFile };

        public static async Task<DriveService> GetServiceAsync(string userEmail, ISettingService settingService)
        {
            if (Services.ContainsKey(userEmail))
                return Services[userEmail];

            // Paths from SettingService
            string credentialPath = settingService.GetGoogleCredentialPath();
            string tokenFolder = settingService.GetGoogleTokenFolder();

            if (!File.Exists(credentialPath))
                throw new FileNotFoundException(
                    "credentials.json missing. Place it in the Credentials folder.",
                    credentialPath
                );

            UserCredential credential;
            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                string tokenPath = Path.Combine(tokenFolder, SanitizeEmail(userEmail));
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    userEmail,
                    CancellationToken.None,
                    new FileDataStore(tokenPath, true)
                );
            }

            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = settingService.GetAppName()
            });

            Services[userEmail] = service;
            return service;
        }

        private static string SanitizeEmail(string email)
        {
            return email.Replace("@", "_").Replace(".", "_");
        }

        public static async Task<string> GetOrCreateFolderAsync(DriveService service, string folderName)
        {
            var listRequest = service.Files.List();
            listRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";
            listRequest.Fields = "files(id, name)";

            var result = await listRequest.ExecuteAsync();

            if (result.Files != null && result.Files.Count > 0)
                return result.Files[0].Id;

            var folderMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            var createRequest = service.Files.Create(folderMetadata);
            createRequest.Fields = "id";

            var createdFolder = await createRequest.ExecuteAsync();
            return createdFolder.Id;
        }
    }
}
