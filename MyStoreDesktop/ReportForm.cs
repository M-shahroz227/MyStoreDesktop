using MyStoreDesktop.Models;
using MyStoreDesktop.Services.BillProductService;
using MyStoreDesktop.Services.BillService;
using MyStoreDesktop.Services.CustomerInvoiceService;
using MyStoreDesktop.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class ReportForm : Form
    {
        private readonly BillService _billService = new BillService();
        private readonly BillProductService _billProductService = new BillProductService();
        private readonly CustomerInvoiceService _customerService = new CustomerInvoiceService();

        private int selectedBillId; // Store selected bill id

        public ReportForm()
        {
            InitializeComponent();

            // Apply professional blue theme
            ThemeManager.ApplyTheme(this);
        }

        private void ReportForm_Load(object sender, EventArgs e)
        { 
            cmbReportType.Items.AddRange(new string[] { "Daily", "Monthly", "Yearly" });
            cmbReportType.SelectedIndex = 0;

            SetupReportGrid();
        }

        private void SetupReportGrid()
        {
            
            dgvReports.Columns.Clear();

            // Hidden BillId column
            var billIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "BillId",
                HeaderText = "BillId",
                Visible = false
            };
            dgvReports.Columns.Add(billIdColumn);

            dgvReports.Columns.Add("Date", "Date");
            dgvReports.Columns.Add("SalesCount", "No. of Sales");
            dgvReports.Columns.Add("TotalSales", "Total Sales");
            dgvReports.Columns.Add("TotalTax", "Total Tax");

            

            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        public void GenerateReport()
        {
            dgvReports.Rows.Clear();

            DateTime start = dtpFrom.Value.Date;
            DateTime end = dtpTo.Value.Date;

            var bills = _billService.GetAll()
                .Where(b => b.BillDate.Date >= start && b.BillDate.Date <= end)
                .ToList();

            decimal grandTotalSales = 0m;
            decimal grandTotalTax = 0m;
            if (cmbReportType.SelectedIndex == null)
            {
                MessageBox.Show("Please select report type first");
            }
            else
            {
                var select = cmbReportType.SelectedItem.ToString();
                IEnumerable<IGrouping<string, Bill>> groupedData = null;

                switch (select)
                {
                    case "Daily":
                        groupedData = bills.GroupBy(b => b.BillDate.ToString("yyyy-MM-dd"));
                        break;
                    case "Monthly":
                        groupedData = bills.GroupBy(b => b.BillDate.ToString("yyyy-MM"));
                        break;
                    case "Yearly":
                        groupedData = bills.GroupBy(b => b.BillDate.ToString("yyyy"));
                        break;
                    default:
                        MessageBox.Show("Invalid Selection");
                        break;
                }
                if(groupedData == null)
                {
                    MessageBox.Show("No data found");
                    return;

                }

                foreach (var group in groupedData)
                {
                    int count = group.Count();
                    decimal totalSales = group.Sum(b => b.GrandTotal);
                    decimal totalTax = group.Sum(b => b.Tax);

                    var firstBill = group.First(); // Get any bill for BillId

                    dgvReports.Rows.Add(
                        firstBill.BillId,        // Hidden BillId
                        group.Key,
                        count,
                        totalSales.ToString("0.00"),
                        totalTax.ToString("0.00"),
                        "View"
                    );

                    grandTotalSales += totalSales;
                    grandTotalTax += totalTax;
                }

                lblTotalSalesValue.Text = grandTotalSales.ToString("C", CultureInfo.CurrentCulture);
                lblTotalTaxValue.Text = grandTotalTax.ToString("C", CultureInfo.CurrentCulture);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }
        private void PrintReport()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 800,
                Height = 600
            };

            previewDialog.ShowDialog();
        }
        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int startX = 20;
            int startY = 20;
            int offsetY = 0;

            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font regularFont = new Font("Arial", 10);

            // Title
            g.DrawString("Sales Report", headerFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 40;

            // Column Headers
            g.DrawString("Date", regularFont, Brushes.Black, startX, startY + offsetY);
            g.DrawString("No. of Sales", regularFont, Brushes.Black, startX + 150, startY + offsetY);
            g.DrawString("Total Sales", regularFont, Brushes.Black, startX + 300, startY + offsetY);
            g.DrawString("Total Tax", regularFont, Brushes.Black, startX + 450, startY + offsetY);
            offsetY += 25;

            // Draw each row from dgvReports
            foreach (DataGridViewRow row in dgvReports.Rows)
            {
                if (!row.IsNewRow)
                {
                    g.DrawString(row.Cells["Date"].Value?.ToString(), regularFont, Brushes.Black, startX, startY + offsetY);
                    g.DrawString(row.Cells["SalesCount"].Value?.ToString(), regularFont, Brushes.Black, startX + 150, startY + offsetY);
                    g.DrawString(row.Cells["TotalSales"].Value?.ToString(), regularFont, Brushes.Black, startX + 300, startY + offsetY);
                    g.DrawString(row.Cells["TotalTax"].Value?.ToString(), regularFont, Brushes.Black, startX + 450, startY + offsetY);
                    offsetY += 25;
                }
            }

            // Print grand totals
            offsetY += 20;
            g.DrawString("Grand Total Sales: " + lblTotalSalesValue.Text, headerFont, Brushes.Black, startX, startY + offsetY);
            offsetY += 25;
            g.DrawString("Grand Total Tax: " + lblTotalTaxValue.Text, headerFont, Brushes.Black, startX, startY + offsetY);
        }


    }

}
