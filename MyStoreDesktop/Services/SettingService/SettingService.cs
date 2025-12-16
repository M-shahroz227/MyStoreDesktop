using MyStoreDesktop.Data;
using MyStoreDesktop.Models; // Assuming your Setting model is here
using System.Collections.Generic;
using System.Linq;

public class SettingService : ISettingService
{
    private readonly DatabaseHelper _context;

    public SettingService()
    {
        _context = new DatabaseHelper();
    }

    public void AddOrUpdate(string key, string value)
    {
        // Look for existing setting in DB
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        if (setting != null)
        {
            setting.Value = value; // Update existing
        }
        else
        {
            setting = new Setting { Key = key, Value = value };
            _context.Settings.Add(setting); // Add new
        }

        _context.SaveChanges(); // Save changes to DB
    }

    public string Get(string key)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        return setting != null ? setting.Value : null;
    }

    public List<Setting> GetAll()
    {
        return _context.Settings.ToList();
    }
}
