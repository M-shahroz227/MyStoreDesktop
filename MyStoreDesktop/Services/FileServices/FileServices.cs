using MyStoreDesktop.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStoreDesktop.Services.FileServices
{
    public class FileServices
    {
        private DatabaseHelper _context;
        private SettingService _settingService;
        public FileServices() 
        {
            _context = new DatabaseHelper();
            _settingService = new SettingService();


        }
        public Image GetFileByName(string imgNameWithExt)
        {
            var basePath = _settingService.GetByKey("BasePath");
            if (!string.IsNullOrEmpty(basePath) && !string.IsNullOrEmpty(imgNameWithExt))
            {
                var fullPath = basePath +@"\"+ imgNameWithExt;
                if (File.Exists(fullPath))
                {
                    return Image.FromFile(fullPath);

                }
                else
                {
                    return null;

                }
            }
            else return null;

        }

        

    }
}
