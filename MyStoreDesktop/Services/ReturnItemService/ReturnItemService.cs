using System;
using System.Collections.Generic;
using System.Linq;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services
{
    public class ReturnItemService : IReturnItemService
    {
        private readonly DatabaseHelper _context;

        public ReturnItemService()
        {
            _context = new DatabaseHelper();
        }

        public void AddReturnItem(ReturnItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var billProduct = _context.BillProducts.FirstOrDefault(b => b.BillProductId == item.BillProductId);
            if (billProduct == null) throw new InvalidOperationException("Bill product not found.");

            if (item.ReturnQuantity > billProduct.Quantity) throw new InvalidOperationException("Return quantity cannot exceed purchased quantity.");

            item.ItemPrice = billProduct.ItemPrice;
            item.TotalPrice = item.ItemPrice * item.ReturnQuantity;

            _context.ReturnItems.Add(item);

            // Stock update
            var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null) product.Quantity += item.ReturnQuantity;

            _context.SaveChanges();
        }

        public List<ReturnItem> GetReturnItemsByReturnId(int returnId)
        {
            return _context.ReturnItems.Where(r => r.ReturnId == returnId).ToList();
        }

        public void DeleteReturnItem(int returnItemId)
        {
            var item = _context.ReturnItems.FirstOrDefault(r => r.ReturnItemId == returnItemId);
            if (item == null) throw new InvalidOperationException("Return item not found.");

            var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null) product.Quantity -= item.ReturnQuantity;

            _context.ReturnItems.Remove(item);
            _context.SaveChanges();
        }
    }
}
