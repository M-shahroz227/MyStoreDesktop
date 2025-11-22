using MyStoreDesktop.Models;
using System.Collections.Generic;

namespace MyStoreDesktop.Services.BillProductService
{
    public interface IBillProductService
    {
        BillProduct Add(BillProduct billProduct);
        void AddRange(List<BillProduct> billProducts);
        List<BillProduct> GetByBillId(int billId);
        List<BillProduct> GetAll();
    }
}
