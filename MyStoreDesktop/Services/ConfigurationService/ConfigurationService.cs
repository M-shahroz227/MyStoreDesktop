using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MyStoreDesktop.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly DatabaseHelper _db;

        public ConfigurationService(DatabaseHelper dbContext)
        {
            _db = dbContext;
        }

        // GET VALUE BY KEY
        public string Get(string key)
        {
            var config = _db.Configurations.FirstOrDefault(c => c.Key == key);
            return config?.Value;
        }

        // ADD OR UPDATE KEY-VALUE SETTING
        public void Set(string key, string value)
        {
            var config = _db.Configurations.FirstOrDefault(c => c.Key == key);

            if (config != null)
            {
                // Update existing
                config.Value = value;
                config.UpdatedAt = DateTime.Now;

                // IMPORTANT FOR EF6
                _db.Entry(config).State = EntityState.Modified;
            }
            else
            {
                // Add new config
                _db.Configurations.Add(new Configuration
                {
                    Key = key,
                    Value = value,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            }

            _db.SaveChanges();
        }

        // CHECK IF KEY EXISTS
        public bool Exists(string key)
        {
            return _db.Configurations.Any(c => c.Key == key);
        }

        // LOAD ALL CONFIG AS DICTIONARY
        public Dictionary<string, string> LoadAll()
        {
            return _db.Configurations.ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
