using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        // ======================= ADD =======================
        public Bill Add(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges();
            return bill;
        }

        // ======================= UPDATE =======================
        public void Update(Bill bill)
        {
            _context.Bills.AddOrUpdate(bill);
            _context.SaveChanges();
        }

        // ======================= DELETE =======================
        public void Delete(int id)
        {
            var bill = _context.Bills.Find(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
            }
        }

        // ======================= GET ALL =======================
        public IEnumerable<Bill> GetAll()
        {
            return _context.Bills.ToList();
        }

        // ======================= GET BY ID =======================
        public Bill GetById(int id)
        {
            return _context.Bills
                .FirstOrDefault(b => b.BillId == id);
        }

        // ======================= ⭐ GET FULL BILL DETAIL =======================
        public Bill GetBillWithDetails(int billId)
        {
            return _context.Bills
                .Include(b => b.CustomerInvoice)
                .Include(b => b.BillProducts)
                .FirstOrDefault(b => b.BillId == billId);
        }

        public void UpdateCustomerInfo(Bill updatedBill)
        {
            // Pehle old bill ko DB se load karo
            var existingBill = _context.Bills
                .Include(b => b.CustomerInvoice)
                .FirstOrDefault(b => b.BillId == updatedBill.BillId);

            if (existingBill == null) return;

            // Update Customer info
            existingBill.CustomerInvoice.CustomerName = updatedBill.CustomerInvoice.CustomerName;
            existingBill.CustomerInvoice.CustomerPhone = updatedBill.CustomerInvoice.CustomerPhone;
            existingBill.CustomerInvoice.CustomerAddress = updatedBill.CustomerInvoice.CustomerAddress;

            // Save
            _context.SaveChanges();
        }

    }
}
