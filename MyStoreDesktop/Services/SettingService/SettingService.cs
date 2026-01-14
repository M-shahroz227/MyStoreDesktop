using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class SettingService : ISettingService
{
    private readonly DatabaseHelper _context;

    public SettingService()
    {
        _context = new DatabaseHelper();
    }

    // ================= BASIC CRUD =================
    public void Add(string key, string value)
    {
        if (_context.Settings.Any(s => s.Key == key))
            throw new InvalidOperationException($"A setting with key '{key}' already exists.");

        _context.Settings.Add(new Setting { Key = key, Value = value });
        _context.SaveChanges();
    }

    public void Update(string key, string value)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        if (setting == null)
            throw new InvalidOperationException($"Setting '{key}' does not exist.");

        setting.Value = value;
        _context.SaveChanges();
    }

    public string GetByKey(string key)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        return setting?.Value;
    }

    public List<Setting> GetAll() => _context.Settings.ToList();

    public void Delete(string key)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        if (setting != null)
        {
            _context.Settings.Remove(setting);
            _context.SaveChanges();
        }
    }

    public Setting GetById(int id) => _context.Settings.FirstOrDefault(s => s.Id == id);

    public void DeleteById(int id)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Id == id);
        if (setting != null)
        {
            _context.Settings.Remove(setting);
            _context.SaveChanges();
        }
    }

    // ================= PROFESSIONAL HELPERS =================
    public string GetAppName() => GetByKey("AppName") ?? "MyStoreDesktop";
    public string GetBasePath() => GetByKey("BasePath") ?? @"C:\MyStore\Backup";
    public string GetStoreName() => GetByKey("StoreName") ?? "My Store";
    public string GetGoogleCredentialPath() =>
        GetByKey("GoogleCredentialPath") ??
        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Credentials", "credentials.json");
    public string GetGoogleTokenFolder() => GetByKey("GoogleTokenFolder") ?? "MyStoreDesktop\\GoogleTokens";
}
