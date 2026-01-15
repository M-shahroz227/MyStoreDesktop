using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public interface IReturnService
    {
        // Create a new return (invoice-level)
        int CreateReturn(Return returnInvoice);

        // Add a return item (product-level)
        void AddReturnItem(ReturnItem item);

        // Get return items by return id
        List<ReturnItem> GetReturnItemsByReturnId(int returnId);

        // Get all return items
        List<ReturnItem> GetAllReturnItems();

        // Remove / cancel a return item
        void DeleteReturnItem(int returnItemId);
    }
}
