using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;
using MyStoreDesktop.Services.BillService;
using MyStoreDesktop.Services.BillProductService;
using MyStoreDesktop.Services.CustomerInvoiceService;
using MyStoreDesktop.Data;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace MyStoreDesktop
{
    public partial class Home : Form
    {
        // Services
        private readonly ProductService _productService = new ProductService();
        private readonly BillService _billService = new BillService();
        private readonly BillProductService _billProductService = new BillProductService();
        private readonly CustomerInvoiceService _customerService = new CustomerInvoiceService();

        // Value holders
        private double _subtotal = 0;
        private double _discountPercent = 0;
        private double _taxPercent = 0;

        public Home()
        {
            InitializeComponent();
            lstSuggestion.Visible = false;

            SetupGridButtons();
            dgvAddToCard.DefaultCellStyle.ForeColor = Color.Black;
            dgvAddToCard.DefaultCellStyle.BackColor = Color.White;
            dgvAddToCard.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            dgvAddToCard.CellContentClick += dgvAddToCard_CellContentClick;
        }

        // ================= GRID SETUP =================
        private void SetupGridButtons()
        {
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "Delete";
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
            deleteButton.UseColumnTextForButtonValue = true;

            if (!dgvAddToCard.Columns.Contains("Delete"))
                dgvAddToCard.Columns.Add(deleteButton);
        }

        // ================= SEARCH =================
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                lstSuggestion.Visible = false;
                return;
            }

            var products = _productService.GetAll()
                .Where(p => p.Title.ToLower().Contains(search)
                         || (p.Model != null && p.Model.ToLower().Contains(search))
                         || (p.Category != null && p.Category.Title.ToLower().Contains(search))
                         || (p.ProductCode != null && p.ProductCode.ToLower().Equals(search))
                         || (p.Company != null && p.Company.Title.ToLower().Contains(search)))
                .ToList();

            lstSuggestion.DataSource = products;
            lstSuggestion.DisplayMember = "Title";
            lstSuggestion.ValueMember = "ProductId";
            lstSuggestion.Visible = products.Any();
        }

        private void lstSuggestion_Click(object sender, EventArgs e)
        {
            if (lstSuggestion.SelectedItem == null)
                return;

            var selectedProduct = (Product)lstSuggestion.SelectedItem;
            AddToCartData(selectedProduct);

            lstSuggestion.Visible = false;
            txtSearch.Clear();
        }

        private void AddToCartData(Product product)
        {
            foreach (DataGridViewRow row in dgvAddToCard.Rows)
            {
                if (Convert.ToInt32(row.Cells["ProductId"].Value) == product.ProductId)
                {
                    int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    int newQty = currentQty + 1;

                    row.Cells["Quantity"].Value = newQty;
                    row.Cells["Total"].Value = newQty * Convert.ToDouble(product.SalePrice);

                    UpdateTotals();
                    return;
                }
            }

            double total = Convert.ToDouble(product.SalePrice);

            dgvAddToCard.Rows.Add(product.ProductId, product.Title, 1, product.SalePrice, product.Discount, total);

            UpdateTotals();
        }

        private void UpdateTotals()
        {
            _subtotal = 0;

            foreach (DataGridViewRow row in dgvAddToCard.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    _subtotal += Convert.ToDouble(row.Cells["Total"].Value);
                }
            }

            double discount = _subtotal * (_discountPercent / 100);
            double afterDiscount = _subtotal - discount;
            double tax = afterDiscount * (_taxPercent / 100);
            double total = afterDiscount + tax;

            lblSubtotalValue.Text = _subtotal.ToString("N2");
            lblDiscountValue.Text = discount.ToString("N2");
            txtTaxValue.Text = tax.ToString("N2");
            lblTotalValue.Text = total.ToString("N2");
        }

        private void UpdateRowTotal(int RowIndex)
        {
            DataGridViewRow row = dgvAddToCard.Rows[RowIndex];

            double price = Convert.ToDouble(row.Cells["SalePrice"].Value);
            double qty = Convert.ToDouble(row.Cells["Quantity"].Value);
            double total = price * qty;

            row.Cells["Total"].Value = total;

            UpdateTotals();
        }

        private void dgvAddToCard_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAddToCard.Columns["Quantity"].Index)
                UpdateRowTotal(e.RowIndex);
        }

        private void dgvAddToCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvAddToCard.Columns[e.ColumnIndex].Name == "Delete")
            {
                dgvAddToCard.Rows.RemoveAt(e.RowIndex);
                UpdateTotals();
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            txtSearch.Text += btn.Text;
        }

        // ================= BILL CREATION =================
        private void BillConfirm_Click(object sender, EventArgs e)
        {
            if (dgvAddToCard.Rows.Count == 0)
            {
                MessageBox.Show("No items in cart!");
                return;
            }

            // ------- 1️⃣ Save Customer -------
            var customer = new CustomerInvoice()
            {
                CustomerName = "Customer Name",
                CustomerPhone = "Phone",
                CustomerAddress = "Address"
            };

            customer = _customerService.Add(customer);

            // ------- 2️⃣ Save Bill -------
            decimal discount = 0;
            decimal tax = 0;
            decimal total = 0;
            decimal subtotal = 0;
            decimal grand = 0;

            decimal.TryParse(lblDiscountValue.Text, out discount);
            decimal.TryParse(txtTaxValue.Text, out tax);
            decimal.TryParse(lblSubtotalValue.Text, out subtotal);
            decimal.TryParse(lblTotalValue.Text, out total);
            decimal.TryParse(lblTotalValue.Text, out grand);

            var bill = new Bill()
            {
                UserId = SessionManager.UserId,
                Role = SessionManager.Role,
                CustomerInvoiceId = customer.Id,
                BillDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                OwnDate = DateTime.Now,
                Discount = discount,
                SalePrice = subtotal,
                TotalAmount = total,
                Tax = tax,
                GrandTotal = grand,
                PaymentMethod = "Cash"
            };


            bill = _billService.Add(bill);

            // ------- 3️⃣ Save Bill Products -------
            List<BillProduct> billProducts = new List<BillProduct>();

            foreach (DataGridViewRow row in dgvAddToCard.Rows)
            {
                if (!row.IsNewRow)
                {
                    billProducts.Add(new BillProduct
                    {
                        BillId = bill.BillId,
                        ProductId = Convert.ToInt32(row.Cells["ProductId"].Value),
                        Title = row.Cells["Title"].Value.ToString(),
                        Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                        ItemPrice = Convert.ToDecimal(row.Cells["SalePrice"].Value),
                        TotalPrice = Convert.ToDecimal(row.Cells["Total"].Value)
                    });
                }
            }

            _billProductService.AddRange(billProducts);

            // ------- 4️⃣ Load Full Bill for Printing -------
            using (var db = new DatabaseHelper())
            {
                var billData = db.Bills
                    .Include("CustomerInvoice")
                    .Include("BillProducts")
                    .FirstOrDefault(x => x.BillId == bill.BillId);

                PrintForm form = new PrintForm(billData);
                form.Show();
            }
        }

        // ================= NAVIGATION =================
        private void LoginPanelReports(object sender, EventArgs e)
        {
            var report = new ReportForm();
            report.Show();
            this.Hide();
        }

        private void LoginPanelSales(object sender, EventArgs e)
        {
            var sales = new SalesForm();
            sales.Show();
            this.Hide();
        }

        private void LoginPanelUsers(object sender, EventArgs e)
        {
            var user = new UserForm();
            user.Show();
            this.Hide();
        }

        private void LoginPanelProduct(object sender, EventArgs e)
        {
            var product = new ProductForm();
            product.Show();
            this.Hide();
        }

        private void LoginPanelbtnHome(object sender, EventArgs e)
        {
            var home = new Home();
            home.Show();
            this.Hide();
        }

        // ================= TAX / DISCOUNT EVENTS =================
        private void txtTaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(txtTaxValue.Text, out double taxPercent))
                    _taxPercent = taxPercent;
                else
                    _taxPercent = 0;

                UpdateTotals();
                e.SuppressKeyPress = true;
            }
        }

        private void lblDiscountValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(lblDiscountValue.Text, out double discountPercent))
                    _discountPercent = discountPercent;
                else
                    _discountPercent = 0;

                UpdateTotals();
                e.SuppressKeyPress = true;
            }
        }
    }
}
