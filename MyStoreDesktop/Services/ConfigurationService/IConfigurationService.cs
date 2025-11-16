using System.Collections.Generic;

namespace MyStoreDesktop.Services.ConfigurationService
{
    public interface IConfigurationService
    {
        string Get(string key);                     // Get value by key
        void Set(string key, string value);         // Add / update key-value
        bool Exists(string key);                    // Check if key exists
        Dictionary<string, string> LoadAll();      // Load all settings
    }
}
