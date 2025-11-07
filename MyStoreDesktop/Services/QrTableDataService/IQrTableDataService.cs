using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.QrTableDataService
{
    public interface IQrTableDataService
    {
        IEnumerable<QrTableData> GetAll();
        QrTableData GetById(int id);
        void Add(QrTableData qrData);
        void Update(QrTableData qrData);
        void Delete(int id);
    }
}
