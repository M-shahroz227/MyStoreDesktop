using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // Setup report type combo box
            cmbReportType.Items.AddRange(new string[] { "Daily", "Monthly", "Yearly" });
            cmbReportType.SelectedIndex = 0;

            SetupReportGrid();
        }

        // ================= GRID SETUP =================
        private void SetupReportGrid()
        {
            dgvReports.Columns.Clear();
            dgvReports.Columns.Add("Date", "Date");
            dgvReports.Columns.Add("SalesCount", "No. of Sales");
            dgvReports.Columns.Add("TotalSales", "Total Sales");
            dgvReports.Columns.Add("TotalTax", "Total Tax");
        }

        // ================= GENERATE REPORT =================
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        public void GenerateReport()
        {
            dgvReports.Rows.Clear();
            Random rnd = new Random();

            DateTime start = dtpFrom.Value.Date;
            DateTime end = dtpTo.Value.Date;

            decimal grandTotalSales = 0m;
            decimal grandTotalTax = 0m;

            for (DateTime d = start; d <= end; d = d.AddDays(1))
            {
                decimal sales = rnd.Next(1000, 5000);
                decimal tax = sales * 0.10m;

                grandTotalSales += sales;
                grandTotalTax += tax;

                dgvReports.Rows.Add(
                    d.ToShortDateString(),
                    rnd.Next(5, 20),
                    sales.ToString("0.00"),
                    tax.ToString("0.00")
                );
            }

            lblTotalSalesValue.Text = grandTotalSales.ToString("C", CultureInfo.CurrentCulture);
            lblTotalTaxValue.Text = grandTotalTax.ToString("C", CultureInfo.CurrentCulture);
        }

        // ================= PRINT REPORT =================
        public void PrintReport()
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument
            };
            preview.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 40;
            int left = 40;

            Font headerFont = new Font("Arial", 20, FontStyle.Bold);
            Font titleFont = new Font("Arial", 12, FontStyle.Bold);
            Font normalFont = new Font("Arial", 11);

            // ===== HEADER =====
            e.Graphics.DrawString("Sales Report", headerFont, Brushes.Black, left, y);
            y += 40;

            e.Graphics.DrawString($"Report Type: {cmbReportType.SelectedItem}", normalFont, Brushes.Black, left, y);
            y += 20;

            e.Graphics.DrawString($"From: {dtpFrom.Value:dd-MM-yyyy}   To: {dtpTo.Value:dd-MM-yyyy}", normalFont, Brushes.Black, left, y);
            y += 30;

            // ===== TABLE HEADER =====
            e.Graphics.DrawString("Date", titleFont, Brushes.Black, left, y);
            e.Graphics.DrawString("Sales Count", titleFont, Brushes.Black, left + 150, y);
            e.Graphics.DrawString("Total Sales", titleFont, Brushes.Black, left + 300, y);
            e.Graphics.DrawString("Total Tax", titleFont, Brushes.Black, left + 450, y);

            y += 25;

            e.Graphics.DrawLine(Pens.Black, left, y, left + 520, y);
            y += 10;

            // ===== TABLE ROWS =====
            foreach (DataGridViewRow row in dgvReports.Rows)
            {
                if (row.IsNewRow) continue;

                e.Graphics.DrawString(row.Cells["Date"].Value.ToString(), normalFont, Brushes.Black, left, y);
                e.Graphics.DrawString(row.Cells["SalesCount"].Value.ToString(), normalFont, Brushes.Black, left + 150, y);
                e.Graphics.DrawString(row.Cells["TotalSales"].Value.ToString(), normalFont, Brushes.Black, left + 300, y);
                e.Graphics.DrawString(row.Cells["TotalTax"].Value.ToString(), normalFont, Brushes.Black, left + 450, y);

                y += 22;
            }

            y += 20;
            e.Graphics.DrawLine(Pens.Black, left, y, left + 520, y);
            y += 20;

            // ===== TOTALS =====
            e.Graphics.DrawString($"Grand Total Sales: {lblTotalSalesValue.Text}", titleFont, Brushes.Black, left, y);
            y += 25;

            e.Graphics.DrawString($"Grand Total Tax: {lblTotalTaxValue.Text}", titleFont, Brushes.Black, left, y);
        }

    }
}
