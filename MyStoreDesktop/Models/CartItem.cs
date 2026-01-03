using System;
using System.Windows.Forms;

namespace MyStoreDesktop.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string UrlImage { get; set; }

        public CartItem()
        {
        }

        public CartItem(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow)
                return;

            ProductId = row.Cells["ProductId"].Value != null
                ? Convert.ToInt32(row.Cells["ProductId"].Value)
                : 0;

            Title = row.Cells["Title"].Value?.ToString() ?? string.Empty;

            Quantity = row.Cells["Quantity"].Value != null
                ? Convert.ToInt32(row.Cells["Quantity"].Value)
                : 0;

            SalePrice = row.Cells["SalePrice"].Value != null
                ? Convert.ToDecimal(row.Cells["SalePrice"].Value)
                : 0m;

            Discount = row.Cells["Discount"].Value != null
                ? Convert.ToDecimal(row.Cells["Discount"].Value)
                : 0m;

            Total = row.Cells["Total"].Value != null
                ? Convert.ToDecimal(row.Cells["Total"].Value)
                : 0m;

            UrlImage = row.Cells["UrlImage"].Value?.ToString() ?? string.Empty;
        }

        public void AddToGrid(DataGridView dgv)
        {
            if (dgv == null)
                return;

            dgv.Rows.Add(ProductId, Title, Quantity, SalePrice, Discount, Total, UrlImage);
        }
    }
}
