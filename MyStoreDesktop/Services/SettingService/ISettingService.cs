using System.Collections.Generic;

public interface ISettingService
{
    // Add a new setting
    void Add(string key, string value);

    // Update an existing setting
    void Update(string key, string value);

    // Read a setting by key
    string GetByKey(string key);

    // Read a setting by Id
    Setting GetById(int id);

    // Read all settings
    List<Setting> GetAll();

    // Delete a setting by key
    void Delete(string key);

    // Delete a setting by Id
    void DeleteById(int id);
}
