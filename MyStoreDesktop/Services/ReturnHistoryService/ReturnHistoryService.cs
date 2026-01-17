using System;
using System.Collections.Generic;
using System.Linq;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public class ReturnHistoryService : IReturnHistoryService
    {
        private readonly DatabaseHelper _context;

        public ReturnHistoryService()
        {
            _context = new DatabaseHelper();
        }

        // ------------------ Get all return history ------------------
        public List<BillHistory> GetAllHistory()
        {
            return _context.BillHistories
                .OrderByDescending(h => h.ModifiedOn)
                .ToList();
        }

        // ------------------ Get specific history by ID ------------------
        public BillHistory GetHistoryById(int historyId)
        {
            return _context.BillHistories
                .FirstOrDefault(h => h.BillHistoryId == historyId);
        }
    }
}
