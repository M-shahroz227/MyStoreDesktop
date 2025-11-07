using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DatabaseHelper _context;

        public ProductService()
        {
            _context = new DatabaseHelper();
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();

        public Product GetById(int id) => _context.Products.Find(id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
