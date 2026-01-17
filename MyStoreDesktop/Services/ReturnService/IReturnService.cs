using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public interface IReturnService
    {
        void ReturnProduct(int billId, int billProductId, string currentUser);
        void ModifyReturnedProduct(int billId, int billProductId, int newQuantity, decimal newPrice, string currentUser);


    }
}
