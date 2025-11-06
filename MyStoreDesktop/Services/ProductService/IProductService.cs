using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
