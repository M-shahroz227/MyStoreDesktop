using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyStoreDesktop.Services.ProductImageService
{
    public class ProductImageService : IProductImageService
    {
        private readonly DatabaseHelper _context;

        public ProductImageService()
        {
            _context = new DatabaseHelper();
        }

        // Single image setting for a product
        public ProductImageSetting GetByProductId(int productId)
        {
            return _context.ProductImageSettings
                           .FirstOrDefault(x => x.ProductId == productId);
        }

        // Add new image setting
        public void Add(ProductImageSetting setting)
        {
            _context.ProductImageSettings.Add(setting);
            _context.SaveChanges();
        }

        // Update existing image setting
        public void Update(ProductImageSetting setting)
        {
            _context.Entry(setting).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        // Get all image settings
        public IEnumerable<ProductImageSetting> GetAll()
        {
            return _context.ProductImageSettings.ToList();
        }

        // Get all image settings for a product (if multiple)
        public IEnumerable<ProductImageSetting> GetByProduct(int productId)
        {
            return _context.ProductImageSettings
                           .Where(x => x.ProductId == productId)
                           .ToList();
        }
    }
}
