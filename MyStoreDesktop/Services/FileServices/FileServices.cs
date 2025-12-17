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
            if (!string.IsNullOrEmpty(imgNameWithExt) && File.Exists(imgNameWithExt))
            {
                return Image.FromFile(imgNameWithExt);
                
            }
            else
            {
                return null;
        
            }
            

        }

    }
}
