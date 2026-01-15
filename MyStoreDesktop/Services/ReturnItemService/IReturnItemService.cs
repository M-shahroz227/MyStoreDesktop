using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public interface IReturnItemService
    {
        void AddReturnItem(ReturnItem item);
        List<ReturnItem> GetReturnItemsByReturnId(int returnId);
        void DeleteReturnItem(int returnItemId);
    }
}
