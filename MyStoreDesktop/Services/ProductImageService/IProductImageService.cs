using MyStoreDesktop.Models;
using System.Collections.Generic;

namespace MyStoreDesktop.Services.ProductImageService
{
    public interface IProductImageService
    {
        // Get single image setting by ProductId
        ProductImageSetting GetByProductId(int productId);

        // Add new image setting
        void Add(ProductImageSetting setting);

        // Update existing image setting
        void Update(ProductImageSetting setting);

        // Get all image settings
        IEnumerable<ProductImageSetting> GetAll();

        // Get all image settings by ProductId (if multiple images per product)
        IEnumerable<ProductImageSetting> GetByProduct(int productId);
    }
}
