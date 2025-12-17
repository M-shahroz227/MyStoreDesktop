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

    // Add a new setting
    // Add a new setting (only if key does not exist)
    public void Add(string key, string value)
    {
        if (_context.Settings.Any(s => s.Key == key))
        {
            throw new InvalidOperationException($"A setting with key '{key}' already exists.");
        }

        var setting = new Setting { Key = key, Value = value };
        _context.Settings.Add(setting);
        _context.SaveChanges();
    }


    // Update an existing setting
    public void Update(string key, string value)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        if (setting == null)
        {
            throw new InvalidOperationException($"Setting with key '{key}' does not exist.");
        }

        setting.Value = value;
        _context.SaveChanges();
    }

    // Get value by key
    public string Get(string key)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        return setting != null ? setting.Value : null;
    }

    // Get setting by Id
    public Setting GetById(int id)
    {
        return _context.Settings.FirstOrDefault(s => s.Id == id);
    }

    // Get all settings
    public List<Setting> GetAll()
    {
        return _context.Settings.ToList();
    }

    // Delete setting by key
    public void Delete(string key)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Key == key);
        if (setting != null)
        {
            _context.Settings.Remove(setting);
            _context.SaveChanges();
        }
    }

    // Delete by Id
    public void DeleteById(int id)
    {
        var setting = _context.Settings.FirstOrDefault(s => s.Id == id);
        if (setting != null)
        {
            _context.Settings.Remove(setting);
            _context.SaveChanges();
        }
    }
}
