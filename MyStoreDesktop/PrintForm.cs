using MyStoreDesktop.Models;
using MyStoreDesktop.Services.BillService;
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

            // Set Thermal Printer Page Size (80mm)
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 300, 1000);

            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDocument;

            // Fix Preview Window Size
            preview.Width = 450;   // small width
            preview.Height = 700;  // tall receipt style
            preview.StartPosition = FormStartPosition.CenterScreen;

            // Fix Zoom
            ((Form)preview).WindowState = FormWindowState.Normal;
            preview.PrintPreviewControl.Zoom = 1.0;  // 100% exact size

            preview.ShowDialog();
        }


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 10;
            int left = 10;

            Font big = new Font("Consolas", 14, FontStyle.Bold);
            Font normal = new Font("Consolas", 10, FontStyle.Regular);

            // ---------- HEADER ----------
            e.Graphics.DrawString("MY STORE", big, Brushes.Black, left + 70, y);
            y += 30;

            e.Graphics.DrawString("---------------------------------------", normal, Brushes.Black, left, y);
            y += 20;

            // ---------- CUSTOMER INFO ----------
            e.Graphics.DrawString($"Customer: {txtName.Text}", normal, Brushes.Black, left, y); y += 20;
            e.Graphics.DrawString($"Phone:    {txtPhone.Text}", normal, Brushes.Black, left, y); y += 20;
            e.Graphics.DrawString($"Date:     {txtDate.Text}", normal, Brushes.Black, left, y); y += 25;
            e.Graphics.DrawString($"Address:    {txtAddress.Text}", normal, Brushes.Black, left, y); y += 20;

            e.Graphics.DrawString("---------------------------------------", normal, Brushes.Black, left, y);
            y += 20;

            // ---------- COLUMN HEADERS ----------
            e.Graphics.DrawString("Item        Qty   Price   Total", normal, Brushes.Black, left, y);
            y += 20;

            e.Graphics.DrawString("---------------------------------------", normal, Brushes.Black, left, y);
            y += 20;

            // ---------- PRODUCTS ----------
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                string name = row.Cells["Title"].Value.ToString();
                string qty = row.Cells["Qty"].Value.ToString();
                string price = row.Cells["Price"].Value.ToString();
                string total = row.Cells["Total"].Value.ToString();

                e.Graphics.DrawString($"{name}", normal, Brushes.Black, left, y);
                y += 15;

                e.Graphics.DrawString($"          {qty}     {price}     {total}", normal, Brushes.Black, left, y);
                y += 20;
            }

            e.Graphics.DrawString("---------------------------------------", normal, Brushes.Black, left, y);
            y += 20;

            // ---------- TOTAL ----------
            e.Graphics.DrawString($"Grand Total: {txtGrandTotal.Text}", big, Brushes.Black, left, y);
            y += 40;

            e.Graphics.DrawString("Thank you for shopping!", normal, Brushes.Black, left + 50, y);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Update bill object from textbox
            _bill.CustomerInvoice.CustomerName = txtName.Text;
            _bill.CustomerInvoice.CustomerPhone = txtPhone.Text;
            _bill.CustomerInvoice.CustomerAddress = txtAddress.Text;

            // Save to database
            BillService service = new BillService();
            service.UpdateCustomerInfo(_bill);

            MessageBox.Show("Customer information updated successfully!");
        }
    }
    
}
