using MyStoreDesktop.Services.BillService;
using MyStoreDesktop.Services.BillProductService;
using MyStoreDesktop.Models;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Services.CustomerInvoiceService;

namespace MyStoreDesktop
{
    public partial class SalesForm : Form
    {
        private readonly BillService _billService = new BillService();
        private readonly BillProductService _billProductService = new BillProductService();
        private readonly CustomerInvoiceService _customerService = new CustomerInvoiceService();


        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            // Setup Grid
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

            LoadSalesFromDatabase();
        }

        // ================================
        // REAL DATABASE SALES LOAD
        // ================================
        private void LoadSalesFromDatabase()
        {
            dgvSales.Rows.Clear();

            var bills = _billService.GetAll().ToList();   // All bills
            var customers = _customerService.GetAll().ToList(); // All customers

            var result = bills.Select(b => new
            {
                b.BillId,
                b.BillDate,
                CustomerName = customers
                    .FirstOrDefault(c => c.Id == b.CustomerInvoiceId)?.CustomerName ?? "Unknown",
                b.GrandTotal
            }).ToList();

            foreach (var sale in result)
            {
                dgvSales.Rows.Add(
                    sale.BillId,
                    sale.BillDate.ToString("yyyy-MM-dd"),
                    sale.CustomerName,
                    sale.GrandTotal
                );
            }

            CalculateTotalSales();
        }


        // ================================
        // FILTER SALES BY DATE
        // ================================
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

        // ================================
        // REFRESH BUTTON
        // ================================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSales.Rows)
                row.Visible = true;

            LoadSalesFromDatabase();
        }

        // ================================
        // CALCULATE TOTAL SALES
        // ================================
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

        // ================================
        // VIEW BUTTON — SHOW BILL PRODUCTS
        // ================================
        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSales.Columns[e.ColumnIndex].Name == "View")
            {
                int billId = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["SaleID"].Value);

                var products = _billProductService.GetByBillId(billId);

                string message = "Bill Products:\n\n";

                foreach (var item in products)
                {
                    message += $"{item.Title}   x {item.Quantity}   = {item.SalePrice}\n";
                }

                MessageBox.Show(message, "Sale Details");
            }
        }
    }
}
