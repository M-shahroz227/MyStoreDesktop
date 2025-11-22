using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MyStoreDesktop.Services.CustomerInvoiceService
{
    public class CustomerInvoiceService : ICustomerInvoiceService
    {
        private readonly DatabaseHelper _context;

        public CustomerInvoiceService()
        {
            _context = new DatabaseHelper();
        }

        public CustomerInvoice Add(CustomerInvoice customer)
        {
            _context.CustomerInvoices.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public CustomerInvoice GetById(int id)
        {
            return _context.CustomerInvoices.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<CustomerInvoice> GetAll()
        {
            return _context.CustomerInvoices.ToList();
        }

        public void Delete(int id)
        {
            var customer = _context.CustomerInvoices.Find(id);
            if (customer != null)
            {
                _context.CustomerInvoices.Remove(customer);
                _context.SaveChanges();
            }
        }

        public void Update(CustomerInvoice customer)
        {
            _context.CustomerInvoices.AddOrUpdate(customer);
            _context.SaveChanges();
        }
    }
}
