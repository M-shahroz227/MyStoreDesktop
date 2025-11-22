using MyStoreDesktop.Models;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class PrintForm : Form
    {
        private Bill _bill;

        public PrintForm(Bill bill)
        {
            InitializeComponent();
            _bill = bill;
            LoadBillInfo();
        }

        private void LoadBillInfo()
        {
            // -------- Customer Info --------
            txtName.Text = _bill.CustomerInvoice.CustomerName;
            txtPhone.Text = _bill.CustomerInvoice.CustomerPhone;
            txtAddress.Text = _bill.CustomerInvoice.CustomerAddress;
            txtDate.Text = _bill.BillDate.ToShortDateString();
            txtGrandTotal.Text = _bill.GrandTotal.ToString();

            // -------- Product Table Fill --------
            dataGridView.DataSource = _bill.BillProducts
                .Select(x => new
                {
                    x.ProductId,
                    Title = x.Title,
                    Qty = x.Quantity,
                    Price = x.ItemPrice,
                    Total = x.TotalPrice
                })
                .ToList();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDocument;
            preview.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 20;

            Font headerFont = new Font("Arial", 18, FontStyle.Bold);
            Font subFont = new Font("Arial", 12, FontStyle.Regular);

            // ---------- HEADER ----------
            e.Graphics.DrawString("My Store Invoice", headerFont, Brushes.Black, 250, y);
            y += 40;

            // ---------- CUSTOMER INFO ----------
            e.Graphics.DrawString("Customer Information", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, y);
            y += 30;

            e.Graphics.DrawString("Name: " + txtName.Text, subFont, Brushes.Black, 20, y); y += 25;
            e.Graphics.DrawString("Phone: " + txtPhone.Text, subFont, Brushes.Black, 20, y); y += 25;
            e.Graphics.DrawString("Date: " + txtDate.Text, subFont, Brushes.Black, 20, y); y += 25;
            e.Graphics.DrawString("Address: " + txtAddress.Text, subFont, Brushes.Black, 20, y); y += 40;

            // ---------- TABLE HEADER ----------
            e.Graphics.DrawString(
                "ProductID     ProductName     Qty     Price     Total",
                new Font("Arial", 12, FontStyle.Bold),
                Brushes.Black,
                20,
                y
            );
            y += 25;

            // ---------- PRODUCTS LOOP ----------
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                string id = row.Cells["ProductId"].Value.ToString();
                string name = row.Cells["Title"].Value.ToString();
                string qty = row.Cells["Qty"].Value.ToString();
                string price = row.Cells["Price"].Value.ToString();
                string total = row.Cells["Total"].Value.ToString();

                e.Graphics.DrawString(
                    $"{id}     {name}     {qty}     {price}     {total}",
                    subFont,
                    Brushes.Black,
                    20,
                    y
                );

                y += 22;
            }

            y += 30;

            // ---------- GRAND TOTAL ----------
            e.Graphics.DrawString(
                "Grand Total: " + txtGrandTotal.Text,
                new Font("Arial", 14, FontStyle.Bold),
                Brushes.Black,
                20,
                y
            );
        }
    }
}
