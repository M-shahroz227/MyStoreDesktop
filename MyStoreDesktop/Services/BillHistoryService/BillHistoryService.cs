using System;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public class BillHistoryService : IBillHistoryService
    {
        private readonly DatabaseHelper _context;

        public BillHistoryService()
        {
            _context = new DatabaseHelper();
        }

        public void SaveHistory(Bill bill, string beforeJson, string afterJson, string currentUser)
        {
            var history = new BillHistory
            {
                BillId = bill.BillId,
                BeforeJson = beforeJson,
                AfterJson = afterJson,
                SnapshotJson = afterJson,
                ModifiedBy = currentUser,
                ModifiedOn = DateTime.Now
            };

            _context.BillHistories.Add(history);
            _context.SaveChanges();
        }
    }
}
