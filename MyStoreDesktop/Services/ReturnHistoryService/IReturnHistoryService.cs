using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public interface IReturnHistoryService
    {
        List<BillHistory> GetAllHistory();
        BillHistory GetHistoryById(int historyId);
    }
}
