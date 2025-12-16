using System.Collections.Generic;

public interface ISettingService
{
    void AddOrUpdate(string key, string value);
    string Get(string key);
    List<Setting> GetAll();
}
