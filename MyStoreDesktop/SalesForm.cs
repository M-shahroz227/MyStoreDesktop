using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            // Setup Sales Grid
            dgvSales.Columns.Clear();
            dgvSales.Columns.Add("SaleID", "Sale ID");
            dgvSales.Columns.Add("Date", "Date");
            dgvSales.Columns.Add("Customer", "Customer");
            dgvSales.Columns.Add("TotalAmount", "Total Amount");

            var viewBtn = new DataGridViewButtonColumn()
            {
                Name = "View",
                Text = "View",
                UseColumnTextForButtonValue = true
            };
            dgvSales.Columns.Add(viewBtn);

            // Dummy Data Example (You can replace this with DB data)
            dgvSales.Rows.Add(1, DateTime.Now.AddDays(-1).ToShortDateString(), "Ali", "2500");
            dgvSales.Rows.Add(2, DateTime.Now.AddDays(-2).ToShortDateString(), "Ahmed", "1800");
            dgvSales.Rows.Add(3, DateTime.Now.ToShortDateString(), "Zara", "3200");

            CalculateTotalSales();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;

            decimal total = 0m;

            foreach (DataGridViewRow row in dgvSales.Rows)
            {
                if (row.IsNewRow) continue;

                DateTime saleDate = DateTime.Parse(row.Cells["Date"].Value.ToString());

                if (saleDate >= fromDate && saleDate <= toDate)
                {
                    row.Visible = true;
                    total += Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                }
                else
                {
                    row.Visible = false;
                }
            }

            lblTotalSalesValue.Text = total.ToString("C", CultureInfo.CurrentCulture);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSales.Rows)
                row.Visible = true;

            CalculateTotalSales();
        }

        private void CalculateTotalSales()
        {
            decimal total = 0m;

            foreach (DataGridViewRow row in dgvSales.Rows)
            {
                if (row.IsNewRow) continue;
                total += Convert.ToDecimal(row.Cells["TotalAmount"].Value);
            }

            lblTotalSalesValue.Text = total.ToString("C", CultureInfo.CurrentCulture);
        }

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSales.Columns[e.ColumnIndex].Name == "View")
            {
                MessageBox.Show($"Viewing details for Sale ID: {dgvSales.Rows[e.RowIndex].Cells["SaleID"].Value}");
            }
        }
    }
}
