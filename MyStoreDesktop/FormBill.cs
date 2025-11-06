using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class FormBill : Form
    {
        private static readonly PrintDocument billPrintDocument = new PrintDocument();
        PrintDocument printDoc = billPrintDocument;
        private DataGridView dgvCart;
        private Label lblTaxValue;

        public FormBill(Label lblTaxValue)
        {
            this.lblTaxValue = lblTaxValue;
        }

        private Label lblSubTotalValue;
        

        public FormBill()
        {
            InitializeComponent();

            dgvCart = new DataGridView();
            // DataGridView Columns
            dgvCart.Columns.Add("ItemName", "Item");
            dgvCart.Columns.Add("Quantity", "Qty");
            dgvCart.Columns.Add("Price", "Price");
            dgvCart.Columns.Add("Total", "Total");

            printDoc.PrintPage += new PrintPageEventHandler(PrintBill);
        }

        private void RecalculateTotals()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;     
                decimal qty = Convert.ToDecimal(row.Cells["Quantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                decimal total = qty * price;
                row.Cells["Total"].Value = total.ToString("0.00");
                subtotal += total;
            }

            decimal tax = subtotal * 0.10m;
            decimal totalAmount = subtotal + tax;

            
            lblTaxValue.Text = tax.ToString("0.00");
            lblTotalValue.Text = totalAmount.ToString("0.00");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvCart.Rows.Add("Mobile Charger", 2, 500, 0);
            RecalculateTotals();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.RemoveAt(dgvCart.SelectedRows[0].Index);
                RecalculateTotals();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sale Confirmed Successfully!");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDoc;
            preview.ShowDialog();
        }

        private void PrintBill(object sender, PrintPageEventArgs e)
        {
            float y = 20;
            e.Graphics.DrawString("MY STORE BILL", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 100, y);
            y += 40;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;
                string line = $"{row.Cells["ItemName"].Value} x{row.Cells["Quantity"].Value} = Rs.{row.Cells["Total"].Value}";
                e.Graphics.DrawString(line, new Font("Arial", 10), Brushes.Black, 50, y);
                y += 20;
            }

            y += 30;
            
            e.Graphics.DrawString($"Tax: {lblTaxValue.Text}", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 50, y);
            y += 20;
            e.Graphics.DrawString($"Total: {lblTotalValue.Text}", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 50, y);
        }
    }
}
