using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;
using MyStoreDesktop.Services.BillService;
using MyStoreDesktop.Services.BillProductService;
using MyStoreDesktop.Services.CustomerInvoiceService;
using MyStoreDesktop.Data;
using MyStoreDesktop.Theme;

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
        private double _sideDiscount = 0; 

        private double _taxPercent = 0;
        private TextBox _selectedBox = null;
        private Timer blinkTimer = new Timer();
        private bool isBlinking = false;





        public Home()
        {
            InitializeComponent();

            // Apply professional blue theme
            ThemeManager.ApplyTheme(this);

            txtSearch.TabIndex = 0;
            dgvAddToCard.TabIndex = 1;
            txtPayment.TabIndex = 2;
            btnConfirm.TabIndex = 3;


            lstSuggestion.Visible = false;
            dgvAddToCard.CellContentClick += dgvAddToCard_CellContentClick;
            dgvAddToCard.CellClick += dgvAddToCard_CellClick;
            txtPayment.TextChanged += txtPayment_TextChanged;
            txtTaxValue.Text = "0";   // ⭐ Default TAX 0
            lblDiscountValue.Text = "0"; // 
            lblDiscountValue.ReadOnly = true;

            dgvAddToCard.Columns["SalePrice"].ReadOnly = false;
            btnConfirm.Enabled = false;
            


            SetupGridButtons();
            dgvAddToCard.ColumnHeadersVisible = true;
            dgvAddToCard.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvAddToCard.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvAddToCard.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
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
            if (!blinkTimer.Enabled)
                blinkTimer.Start();

            string search = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                lstSuggestion.Visible = false;
                return;
            }

            var products = _productService.GetAll()
    .Where(p =>
           p.Title.ToLower().Contains(search)
        || (p.Model != null && p.Model.ToLower().Contains(search))
        || (p.Category != null && p.Category.Title.ToLower().Contains(search))
        || (p.ProductCode != null && p.ProductCode.ToLower().Equals(search))
        || (p.CodeType == 1 && p.ProductCode.ToLower().Equals(search))   // 🔥 QR Code
        || (p.CodeType == 2 && p.ProductCode.ToLower().Equals(search))   // 🔥 Barcode
        || (p.Company != null && p.Company.Title.ToLower().Contains(search))
    )
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

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            // Start blinking when the TextBox gets focus
            blinkTimer.Start();
        }


        private void txtSearch_Leave(object sender, EventArgs e)
        {
            blinkTimer.Stop();
            txtSearch.BackColor = Color.White;
        }



        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            txtSearch.BackColor = Color.White; // always white
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
                    CheckButtonsAccess();  // Add here
                    return;
                }
            }

            double total = Convert.ToDouble(product.SalePrice);

            dgvAddToCard.Rows.Add(product.ProductId, product.Title, 1, product.SalePrice, product.Discount, total);

            UpdateTotals();
            CheckButtonsAccess(); // ✅ Add here too
        }
        private void CalculateChange()
        {
            double payment = 0;
            double total = 0;

            double.TryParse(txtPayment.Text, out payment);
            double.TryParse(lblTotalValue.Text, out total);

            double change = payment - total;

            if (change < 0)
                change = 0;

            txtChange.Text = change.ToString("N2");
        }
        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }



        private void UpdateTotals()
        {
            _subtotal = 0;
            double gridDiscount = 0;

            // 1️⃣ Subtotal & Row Discount collect
            foreach (DataGridViewRow row in dgvAddToCard.Rows)
            {
                if (!row.IsNewRow)
                {
                    double rowTotal = 0;
                    double rowDiscount = 0;

                    double.TryParse(row.Cells["Total"].Value?.ToString(), out rowTotal);
                    double.TryParse(row.Cells["Discount"].Value?.ToString(), out rowDiscount);

                    _subtotal += rowTotal;
                    gridDiscount = rowDiscount;

                }
            }

            // 2️⃣ Side Discount also read
            double sideDiscount = 0;
            double.TryParse(lblDiscountValue.Text, out sideDiscount);

            double totalDiscount = gridDiscount + sideDiscount;

            if (totalDiscount > _subtotal)
                totalDiscount = _subtotal;

            // 3️⃣ Tax
            double tax = 0;
            double.TryParse(txtTaxValue.Text, out tax);

            double totalAfterDiscount = _subtotal - totalDiscount;
            double taxAmount = totalAfterDiscount * (tax / 100.0);
            double finalTotal = totalAfterDiscount + taxAmount;

            // 4️⃣ UI update
            lblSubtotalValue.Text = _subtotal.ToString("N2");
            lblDiscountValue.Text = totalDiscount.ToString("N2");
            lblTotalValue.Text = finalTotal.ToString("N2");

            // 5️⃣ Change
            CalculateChange();
        }



        private void UpdateRowTotal(int rowIndex)
        {
            DataGridViewRow row = dgvAddToCard.Rows[rowIndex];

            double qty = Convert.ToDouble(row.Cells["Quantity"].Value);
            double price = Convert.ToDouble(row.Cells["SalePrice"].Value);

            double discount = 0;
            if (row.Cells["Discount"].Value != null)
                double.TryParse(row.Cells["Discount"].Value.ToString(), out discount);

            double total = (qty * price) - discount;

            if (total < 0)
                total = 0;

            row.Cells["Total"].Value = total.ToString("N2");

            UpdateTotals();
        }


        private void dgvAddToCard_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAddToCard.Columns["Quantity"].Index ||
                e.ColumnIndex == dgvAddToCard.Columns["SalePrice"].Index ||
                e.ColumnIndex == dgvAddToCard.Columns["Discount"].Index)   // ✅ NEW
            {
                
                UpdateRowTotal(e.RowIndex);
            }
        }

        private void dgvAddToCard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 &&
                e.ColumnIndex == dgvAddToCard.Columns["SalePrice"].Index)
            {
                dgvAddToCard.BeginEdit(true);
            }
        }

        private void dgvAddToCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvAddToCard.Columns[e.ColumnIndex].Name == "Delete")
            {
                DataGridViewRow row = dgvAddToCard.Rows[e.RowIndex];
                if (row.IsNewRow)
                    return;
                dgvAddToCard.Rows.RemoveAt(e.RowIndex);
                UpdateTotals();
                CheckButtonsAccess();   // <-- button disable enable here
            }

        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (_selectedBox == null)
            {
                MessageBox.Show("Please select a field first (Tax, Discount, Payment, Total)");
                return;
            }

            Button btn = sender as Button;

            if (btn != null)
            {
                _selectedBox.Text += btn.Text;
            }
            UpdateTotals();

        }
        private void txtTaxValue_Click(object sender, EventArgs e)
        {
            _selectedBox = txtTaxValue;
        }

        private void lblDiscountValue_Click(object sender, EventArgs e)
       {
            _selectedBox = lblDiscountValue as TextBox;
        }

        private void txtPayment_Click(object sender, EventArgs e)
        {
            _selectedBox = txtPayment;
        }

        private void lblTotalValue_Click(object sender, EventArgs e)
        {
            _selectedBox = lblTotalValue as TextBox;
        }
       
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_selectedBox != null)
            {
                _selectedBox.Clear();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_selectedBox != null && _selectedBox.Text.Length > 0)
            {
                _selectedBox.Text = _selectedBox.Text.Substring(0, _selectedBox.Text.Length - 1);
            }
        }




        // ================= BILL CREATION =================
        private void BillConfirm_Click(object sender, EventArgs e)
        {
            int realRows = dgvAddToCard.Rows
                 .Cast<DataGridViewRow>()
                 .Count(r => !r.IsNewRow);

            if (realRows == 0)
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
                itemPrice = subtotal,
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
              var BillData = _billService.GetBillWithDetails(bill.BillId);
            PrintForm printForm = new PrintForm(BillData,this);
            printForm.ShowDialog();
        }
        // ================= NAVIGATION =================
        private void LoginPanelReports(object sender, EventArgs e)
        {
            var report = new ReportForm();
            report.Show();
            
        }

        private void LoginPanelSales(object sender, EventArgs e)
        {
            var sales = new SalesForm();
            sales.Show();
            
        }

        private void LoginPanelUsers(object sender, EventArgs e)
        {
            var user = new UserForm();
            user.Show();
            
        }

        private void LoginPanelProduct(object sender, EventArgs e)
        {
            var product = new ProductForm();
            product.Show();
           
        }

        private void LoginPanelbtnHome(object sender, EventArgs e)
        {
            var home = new Home();
            home.Show();
           
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
        public void ClearCart()
        {
            dgvAddToCard.Rows.Clear();
            lblSubtotalValue.Text = "0";
            lblDiscountValue.Text = "0";
            txtTaxValue.Text = "0";
            lblTotalValue.Text = "0";
            CheckButtonsAccess();
        }
        private void CheckButtonsAccess()
        {
            bool hasRows = dgvAddToCard.Rows
                            .Cast<DataGridViewRow>()
                            .Any(r => !r.IsNewRow);

            btnConfirm.Enabled = hasRows;
            
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // TAB → Move to grid
            if (e.KeyCode == Keys.Tab)
            {
                if (dgvAddToCard.Rows.Count > 0)
                {
                    dgvAddToCard.Focus();
                    dgvAddToCard.CurrentCell = dgvAddToCard.Rows[0].Cells[1];
                }
                e.SuppressKeyPress = true;
                return;
            }

            // ENTER → Select product from list
            if (e.KeyCode == Keys.Enter)
            {
                if (lstSuggestion.Visible && lstSuggestion.Items.Count > 0)
                {
                    var selectedProduct = (Product)lstSuggestion.SelectedItem;

                    AddToCartData(selectedProduct);

                    lstSuggestion.Visible = false;
                    txtSearch.Clear();

                    e.SuppressKeyPress = true; // block ding sound
                }
            }
        }

        private void dgvAddToCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPayment.Focus();
                e.SuppressKeyPress = true;
            }
        }
        private void txtPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnConfirm.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Hide();


        }
    }
}
