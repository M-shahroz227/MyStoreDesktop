using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.QrTableDataService
{
    public interface IQrTableDataService
    {
        IEnumerable<QrTableData> GetAll();
        QrTableData GetById(int id);

        // Get all QR / Barcode / Manual codes of a specific product
        IEnumerable<QrTableData> GetByProduct(int productId);

        void Add(QrTableData qrData);
        void Update(QrTableData qrData);
        void Delete(int id);
    }
}
