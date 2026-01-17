using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public interface IBillHistoryService
    {
        void SaveHistory(Bill bill, string beforeJson, string afterJson, string currentUser);
    }
}
