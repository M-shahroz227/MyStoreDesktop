using System.Collections.Generic;

public interface ISettingService
{
    // ================= BASIC CRUD =================
    void Add(string key, string value);
    void Update(string key, string value);
    string GetByKey(string key);
    Setting GetById(int id);
    List<Setting> GetAll();
    void Delete(string key);
    void DeleteById(int id);

    // ================= PROFESSIONAL HELPERS =================
    string GetAppName();                // e.g. "MyStoreDesktop"
    string GetBasePath();               // e.g. "C:\MyStore\Backup"
    string GetStoreName();              // e.g. "My Store"
    string GetGoogleCredentialPath();   // e.g. "C:\MyStore\Credentials\credentials.json"
    string GetGoogleTokenFolder();      // e.g. "C:\Users\User\AppData\GoogleTokens"
}
