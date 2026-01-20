using System;
using System.Linq;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using Newtonsoft.Json;

namespace MyStoreDesktop.Services
{
    public class ReturnService : IReturnService
    {
        private readonly DatabaseHelper _context;
        private readonly IBillHistoryService _historyService;

        public ReturnService()
        {
            _context = new DatabaseHelper();
            _historyService = new BillHistoryService();
        }

        // ------------------ RETURN PRODUCT ------------------
        public void ReturnProduct(int billId, int billProductId, string currentUser)
        {
            var bill = _context.Bills.FirstOrDefault(b => b.BillId == billId);
            if (bill == null) throw new Exception("Bill not found");

            var billProduct = _context.BillProducts.FirstOrDefault(bp => bp.BillProductId == billProductId && bp.BillId == billId);
            if (billProduct == null) throw new Exception("BillProduct not found");
            if (billProduct.IsReturn) throw new Exception("Product already returned");

            // Before snapshot
            var beforeProducts = _context.BillProducts
                .Where(bp => bp.BillId == billId)
                .Select(bp => new { bp.BillProductId, bp.ProductId, bp.Quantity, bp.Price, bp.IsReturn })
                .ToList();
            string beforeJson = JsonConvert.SerializeObject(beforeProducts);

            // Return process
            billProduct.IsReturn = true;
            decimal productAmount = billProduct.Price * billProduct.Quantity;
            bill.GrandTotal -= productAmount;
            if (bill.GrandTotal < 0) bill.GrandTotal = 0;

            var product = _context.Products.FirstOrDefault(p => p.ProductId == billProduct.ProductId);
            if (product != null) product.Quantity += billProduct.Quantity;

            _context.SaveChanges();

            // After snapshot
            var afterProducts = _context.BillProducts
                .Where(bp => bp.BillId == billId)
                .Select(bp => new { bp.BillProductId, bp.ProductId, bp.Quantity, bp.Price, bp.IsReturn })
                .ToList();
            string afterJson = JsonConvert.SerializeObject(afterProducts);

            // Save history
            _historyService.SaveHistory(bill, beforeJson, afterJson, currentUser);
        }

        // ------------------ MODIFY RETURNED PRODUCT ------------------
        public void ModifyReturnedProduct(int billId, int billProductId, int newQuantity, decimal newPrice, string currentUser)
        {
            var bill = _context.Bills.FirstOrDefault(b => b.BillId == billId);
            if (bill == null) throw new Exception("Bill not found");

            var billProduct = _context.BillProducts.FirstOrDefault(bp => bp.BillProductId == billProductId && bp.BillId == billId);
            if (billProduct == null) throw new Exception("BillProduct not found");
            if (!billProduct.IsReturn) throw new Exception("Product is not marked as returned");

            // Before snapshot
            var beforeProducts = _context.BillProducts
                .Where(bp => bp.BillId == billId)
                .Select(bp => new { bp.BillProductId, bp.ProductId, bp.Quantity, bp.Price, bp.IsReturn })
                .ToList();
            string beforeJson = JsonConvert.SerializeObject(beforeProducts);

            // Adjust grand total (remove old amount, add new amount)
            decimal oldAmount = billProduct.Price * billProduct.Quantity;
            decimal newAmount = newPrice * newQuantity;
            bill.GrandTotal = bill.GrandTotal - oldAmount + newAmount;
            if (bill.GrandTotal < 0) bill.GrandTotal = 0;

            // Adjust product stock
            var product = _context.Products.FirstOrDefault(p => p.ProductId == billProduct.ProductId);
            if (product != null)
            {
                int stockChange = newQuantity - billProduct.Quantity;
                product.Quantity += stockChange;
            }

            // Update returned product
            billProduct.Quantity = newQuantity;
            billProduct.Price = newPrice;

            _context.SaveChanges();

            // After snapshot
            var afterProducts = _context.BillProducts
                .Where(bp => bp.BillId == billId)
                .Select(bp => new { bp.BillProductId, bp.ProductId, bp.Quantity, bp.Price, bp.IsReturn })
                .ToList();
            string afterJson = JsonConvert.SerializeObject(afterProducts);

            // Save history
            _historyService.SaveHistory(bill, beforeJson, afterJson, currentUser);
        }
        public BillProduct GetBillProduct(int billId, int billProductId)
        {
            return _context.BillProducts
                           .FirstOrDefault(bp => bp.BillId == billId && bp.BillProductId == billProductId);
        }
    }
}
