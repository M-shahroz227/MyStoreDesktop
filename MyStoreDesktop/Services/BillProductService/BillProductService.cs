using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace MyStoreDesktop.Services.BillProductService
{
    public class BillProductService : IBillProductService
    {
        private readonly DatabaseHelper _DatabaseHelper;

        public BillProductService()
        {
            _DatabaseHelper = new DatabaseHelper();
        }

        public BillProduct Add(BillProduct billProduct)
        {
            _DatabaseHelper.BillProducts.Add(billProduct);
            _DatabaseHelper.SaveChanges();
            return billProduct;
        }

        public void AddRange(List<BillProduct> billProducts)
        {
            _DatabaseHelper.BillProducts.AddRange(billProducts);
            _DatabaseHelper.SaveChanges();
        }

        public List<BillProduct> GetByBillId(int billId)
        {
            return _DatabaseHelper.BillProducts
                .Where(x => x.BillId == billId)
                .ToList();
        }

        public List<BillProduct> GetAll()
        {
            return _DatabaseHelper.BillProducts.ToList();
        }
        public List<BillProduct> GetBillProductsByBillId(int billId)
        {
            return _DatabaseHelper.BillProducts
                .Include(bp => bp.Product)     // Product table ka FK relation
                .Where(bp => bp.BillId == billId)
                .ToList();
        }

    }
}
