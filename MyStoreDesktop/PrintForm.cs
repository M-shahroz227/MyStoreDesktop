using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyStoreDesktop
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            PrintPreviewDialog printPreview = new PrintPreviewDialog();
            printPreview.Document = printDocument;
            printPreview.ShowDialog();
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 20; // Start print from top

            Font headerFont = new Font("Arial", 18, FontStyle.Bold);
            Font subFont = new Font("Arial", 12, FontStyle.Regular);

            // ******** HEADER ********
            e.Graphics.DrawString("My Store Invoice", headerFont, Brushes.Black, 250, y);
            y += 40;

            // ******** CUSTOMER INFO ********
            e.Graphics.DrawString("Customer Information:", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, 20, y);
            y += 30;

            e.Graphics.DrawString("Name: " + txtName.Text, subFont, Brushes.Black, 20, y);
            y += 25;

            e.Graphics.DrawString("Phone: " + txtPhone.Text, subFont, Brushes.Black, 20, y);
            y += 25;

            e.Graphics.DrawString("Date: " + txtDate.Text, subFont, Brushes.Black, 20, y);
            y += 25;

            e.Graphics.DrawString("Address: " + txtAddress.Text, subFont, Brushes.Black, 20, y);
            y += 40;

            // ******** TABLE HEADER ********
            e.Graphics.DrawString("ProductID    ProductName      Qty     Price    Discount   Tax    Total",
                new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 20, y);

            y += 25;

            // ******** LOOP DATA GRID VIEW ROWS ********
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue;

                string productId = row.Cells["productId"].Value.ToString();
                string productName = row.Cells["productName"].Value.ToString();
                string qty = row.Cells["quantity"].Value.ToString();
                string price = row.Cells["price"].Value.ToString();
                string discount = row.Cells["discount"].Value.ToString();
                string tax = row.Cells["tax"].Value.ToString();
                string total = row.Cells["total"].Value.ToString();

                e.Graphics.DrawString(
                    $"{productId}     {productName}     {qty}     {price}     {discount}     {tax}     {total}",
                    subFont,
                    Brushes.Black,
                    20,
                    y
                );

                y += 22;
            }

            y += 30;

            // ******** GRAND TOTAL ********
            e.Graphics.DrawString("Grand Total: " + txtGrandTotal.Text,
                new Font("Arial", 14, FontStyle.Bold),
                Brushes.Black,
                20,
                y);
        }


    }
}
