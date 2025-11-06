using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.BillService
{
    public interface IBillService
    {
        IEnumerable<Bill> GetAll();
        Bill GetById(int id);
        void Add(Bill bill);
        void Update(Bill bill);
        void Delete(int id);
    }
}
