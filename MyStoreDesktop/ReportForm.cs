using System;
using System.Data;
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
            cmbReportType.Items.AddRange(new string[] { "Daily", "Monthly", "Yearly" });
            cmbReportType.SelectedIndex = 0;

            SetupReportGrid();
        }

        private void SetupReportGrid()
        {
            dgvReports.Columns.Clear();
            dgvReports.Columns.Add("Date", "Date");
            dgvReports.Columns.Add("SalesCount", "No. of Sales");
            dgvReports.Columns.Add("TotalSales", "Total Sales");
            dgvReports.Columns.Add("TotalTax", "Total Tax");
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Dummy data (replace with DB data later)
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

                dgvReports.Rows.Add(d.ToShortDateString(), rnd.Next(5, 20), sales.ToString("0.00"), tax.ToString("0.00"));
            }

            lblTotalSalesValue.Text = grandTotalSales.ToString("C", CultureInfo.CurrentCulture);
            lblTotalTaxValue.Text = grandTotalTax.ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
