using MyStoreDesktop.Models;
using System.Collections.Generic;

namespace MyStoreDesktop.Services.CustomerInvoiceService
{
    public interface ICustomerInvoiceService
    {
        CustomerInvoice Add(CustomerInvoice customer);
        CustomerInvoice GetById(int id);
        IEnumerable<CustomerInvoice> GetAll();
        void Delete(int id);
        void Update(CustomerInvoice customer);
    }
}
