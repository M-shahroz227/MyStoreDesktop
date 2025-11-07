using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);

        // 🔹 Return type change kiya (void → Product)
        Product Add(Product product);

        void Update(Product product);
        void Delete(int id);
    }
}
