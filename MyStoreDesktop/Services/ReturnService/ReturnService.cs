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

        public ReturnService( )
        {
            _context = new DatabaseHelper();
            _historyService = new BillHistoryService();
        }

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

            // Save history via BillHistoryService
            _historyService.SaveHistory(bill, beforeJson, afterJson, currentUser);
        }
    }
}
