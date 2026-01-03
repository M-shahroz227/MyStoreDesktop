using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStoreDesktop.Models
{
    public class BillSessionState
    {
        // Cart Data
        public List<CartItem> CartItems { get; set; }

        // Financial Values
        public double Subtotal { get; set; }
        public double DiscountPercent { get; set; }
        public double SideDiscount { get; set; }
        public double TaxPercent { get; set; }
        public double Total { get; set; }
        public double Payment { get; set; }
        public double Change { get; set; }

        // UI State
        public string SearchText { get; set; }
        public int SelectedRowIndex { get; set; }

        // Tab Metadata
        public int TabNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDirty { get; set; }

        public BillSessionState(int tabNumber)
        {
            TabNumber = tabNumber;
            CartItems = new List<CartItem>();
            CreatedAt = DateTime.Now;
            Subtotal = 0;
            DiscountPercent = 0;
            SideDiscount = 0;
            TaxPercent = 0;
            Total = 0;
            Payment = 0;
            Change = 0;
            SearchText = string.Empty;
            SelectedRowIndex = -1;
            IsDirty = false;
        }

        public void MarkDirty()
        {
            IsDirty = CartItems != null && CartItems.Any();
        }
    }
}
