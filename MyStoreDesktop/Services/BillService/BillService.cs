using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MyStoreDesktop.Services.BillService
{
    internal class BillService : IBillService
    {
        private readonly DatabaseHelper _context;

        public BillService()
        {
            _context = new DatabaseHelper();
        }

        public Bill Add(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges(); // ⭐ ضروری
            return bill;
        }

        public void Delete(int id)
        {
            var bill = _context.Bills.Find(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Bill> GetAll()
        {
            return _context.Bills.ToList();
        }

        public Bill GetById(int id)
        {
            return _context.Bills.FirstOrDefault(b => b.BillId == id);
        }

        public void Update(Bill bill)
        {
            _context.Bills.AddOrUpdate(bill);
            _context.SaveChanges(); // ⭐ ضروری
        }
    }
}
