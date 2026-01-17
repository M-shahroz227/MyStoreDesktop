using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public interface IReturnService
    {
        void ReturnProduct(int billId, int billProductId, string currentUser);
    }
}
