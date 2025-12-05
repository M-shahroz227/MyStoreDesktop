using MyStoreDesktop.Models;
using MyStoreDesktop.Services.BillProductService;
using MyStoreDesktop.Services.BillService;
using MyStoreDesktop.Services.CustomerInvoiceService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

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
            this.Load += BDVdataGridView_Load;
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            // Setup Grid
            BillDetailsViewPanel.Visible = false;
            dgvSales.Columns.Clear();
            dgvSales.Columns.Add("BillId", "Sale ID");
            dgvSales.Columns.Add("Date", "Date");
            dgvSales.Columns.Add("Customer", "Customer");
            dgvSales.Columns.Add("TotalAmount", "Total Amount");
            dgvSales.ColumnHeadersVisible = true;
            dgvSales.ColumnHeadersDefaultCellStyle.BackColor=Color.LightGray;
            dgvSales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;


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
        private int selectedBillId = 0;

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSales.Columns[e.ColumnIndex].Name == "View")
            {
                selectedBillId = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["BillId"].Value);

                dgvSales.Visible = false;
                BillDetailsViewPanel.Visible = true;

                LoadCustomerInfo(selectedBillId);       // <-- Customer Info Load
                LoadBillDataGridView(selectedBillId);   // <-- Bill Products Load
            }
        }

        private void BDVdataGridView_Load(object sender, EventArgs e)
        {
            BDVdataGridView.Columns.Clear();
            BDVdataGridView.Columns.Add("ProductId", "ProductId");
            BDVdataGridView.Columns.Add("Title", "Title");

            BDVdataGridView.Columns.Add("Quantity", "Quantity");
            BDVdataGridView.Columns.Add("SalePrice", "SalePrice");
            BDVdataGridView.Columns.Add("subtotal", "subtotal");



        }
        private void LoadBillDataGridView(int billId)
        {
            BDVdataGridView.Rows.Clear();

            // Bill + BillProduct + Product (FK)
            var bill = _billService.GetAll().FirstOrDefault(x => x.BillId == billId);

            if (bill == null)
                return;

            // BillProduct + Product
            var billProducts = _billProductService.GetBillProductsByBillId(billId);
            

            foreach (var p in billProducts)
            {
                BDVdataGridView.Rows.Add(
                    p.ProductId,
                    p.Product?.Title,            // FK Product table se
                    p.Quantity,
                    p.Product?.SalePrice,        // FK Product se
                    p.TotalPrice
                );
            }

            // Bill Summary Fields
            txtGrandTotal.Text = bill.TotalAmount.ToString();
            txtDiscount.Text = bill.Discount.ToString();
            txtTax.Text = bill.Tax.ToString();
            TxTotal.Text = bill.GrandTotal.ToString();
        }

        private void LoadCustomerInfo(int billId)
        {
            var bill = _billService.GetAll()
                        .FirstOrDefault(b => b.BillId == billId);

            if (bill == null)
                return;

            var customer = _customerService.GetAll()
                             .FirstOrDefault(c => c.Id == bill.CustomerInvoiceId);

            if (customer != null)
            {
                txtCustomerName.Text = customer.CustomerName;
                txtCustomerPhone.Text = customer.CustomerPhone;
                txtCustomerAddress.Text = customer.CustomerAddress;
            }

            // Bill Date from Bill table
            txtCustomerDate.Text = bill.BillDate.ToString("yyyy-MM-dd");
        }



        private void BDVBtnClose_Click(object sender, EventArgs e)
        {
            BillDetailsViewPanel.Hide();
            dgvSales.Visible = true;

        }
       

    }
}
