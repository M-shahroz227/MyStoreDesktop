using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class Home : Form
    {
        private readonly ProductService productService = new ProductService();
        private Form activeForm = null;

        public Home()
        {
            InitializeComponent();
        }

        // Helper method to load child forms into main panel
        private void LoadFormInPanel(Form childForm)
        {
            // Dispose previous form if exists
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(childForm);
            panelMainContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // 🔹 FORM LOAD
        private void Home_Load(object sender, EventArgs e)
        {
            SetupCartGrid();
            SetupEventHandlers();
            RecalculateTotals();
            LoadAvliableProduct();
        }
        private void LoadAvliableProduct() {
            // Dummy data for available products
            dgvProductsAvailable.Rows.Clear();
            foreach (var product in productService.GetAll())
            {
                dgvProductsAvailable.Rows.Add(
                    product.ProductId,
                    product.Title,
                     product.Category.Title,
                     product.Company.Title,
                    product.Quantity,
                    product.SalePrice,
                    product.PurchasePrice,
                    product.Discount,
                    product.Model,
                    product.Description,
                    product.UrlImage);
            }

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            foreach (DataGridViewRow row in dgvProductsAvailable.Rows)
            {
                string title = row.Cells["ItemName"].Value.ToString().ToLower();
                string model = row.Cells["Size"].Value.ToString().ToLower();
                row.Visible = title.Contains(searchText) || model.Contains(searchText);
            }
        }


        // 🔹 GRID SETUP
        private void SetupCartGrid()
        {
            dgvProductsAvailable.Columns.Clear();
            dgvProductsAvailable.AllowUserToAddRows = true;
            dgvProductsAvailable.AllowUserToDeleteRows = true;
            dgvProductsAvailable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvProductsAvailable.Columns.Add("ItemName", "Item");
            dgvProductsAvailable.Columns.Add("Size", "Size");
            dgvProductsAvailable.Columns.Add("Quantity", "Qty");
            dgvProductsAvailable.Columns.Add("UnitPrice", "Price");
            dgvProductsAvailable.Columns.Add("Total", "Total");

            var editBtn = new DataGridViewButtonColumn()
            {
                Name = "Edit",
                Text = "✎",
                UseColumnTextForButtonValue = true,
                Width = 50
            };

            var delBtn = new DataGridViewButtonColumn()
            {
                Name = "Delete",
                Text = "✖",
                UseColumnTextForButtonValue = true,
                Width = 50
            };

            dgvProductsAvailable.Columns.Add(editBtn);
            dgvProductsAvailable.Columns.Add(delBtn);

            dgvProductsAvailable.Columns["Quantity"].ValueType = typeof(int);
            dgvProductsAvailable.Columns["UnitPrice"].ValueType = typeof(decimal);
            dgvProductsAvailable.Columns["Total"].ReadOnly = true;
        }

        // 🔹 EVENT HANDLERS SETUP
        private void SetupEventHandlers()
        {
            dgvProductsAvailable.CellValueChanged += (s, e) => RecalculateTotals();
            dgvProductsAvailable.RowsAdded += (s, e) => RecalculateTotals();
            dgvProductsAvailable.RowsRemoved += (s, e) => RecalculateTotals();

            dgvProductsAvailable.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvProductsAvailable.IsCurrentCellDirty)
                    dgvProductsAvailable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            txtDiscount.TextChanged += (s, e) => RecalculateTotals();
            btnConfirm.Click += BtnConfirm_Click;
            btnClear.Click += BtnClear_Click;
            btnLogout.Click += BtnLogout_Click;

            // Left menu buttons
            btnHome.Click += BtnHome_Click;
            btnProducts.Click += BtnProducts_Click;
            btnUsers.Click += BtnUsers_Click;
            btnSales.Click += BtnSales_Click;
            btnReports.Click += BtnReports_Click;

            // Numeric keypad
            btnNum0.Click += (s, e) => AppendDiscount("0");
            btnNum1.Click += (s, e) => AppendDiscount("1");
            btnNum2.Click += (s, e) => AppendDiscount("2");
            btnNum3.Click += (s, e) => AppendDiscount("3");
            btnNum4.Click += (s, e) => AppendDiscount("4");
            btnNum5.Click += (s, e) => AppendDiscount("5");
            btnNum6.Click += (s, e) => AppendDiscount("6");
            btnNum7.Click += (s, e) => AppendDiscount("7");
            btnNum8.Click += (s, e) => AppendDiscount("8");
            button2.Click += (s, e) => AppendDiscount("9");
            btnDot.Click += (s, e) => AppendDiscount(".");
        }

        // 🔹 RECALCULATE TOTALS
        private void RecalculateTotals()
        {
            decimal subtotal = 0m;

            foreach (DataGridViewRow row in dgvProductsAvailable.Rows)
            {
                if (row.IsNewRow) continue;

                decimal qty = Convert.ToDecimal(row.Cells["Quantity"].Value ?? 0);
                decimal price = Convert.ToDecimal(row.Cells["UnitPrice"].Value ?? 0);
                decimal total = qty * price;

                row.Cells["Total"].Value = total.ToString("0.00");
                subtotal += total;
            }

            decimal discountPercent = 0m;
            decimal.TryParse(txtDiscount.Text, out discountPercent);
            decimal discountAmount = subtotal * (discountPercent / 100m);
            decimal afterDiscount = subtotal - discountAmount;
            decimal tax = afterDiscount * 0.10m;
            decimal totalAmount = afterDiscount + tax;

            lblSubTotalValue.Text = subtotal.ToString("C", CultureInfo.CurrentCulture);
            lblTaxValue.Text = tax.ToString("C", CultureInfo.CurrentCulture);
            lblTotalValue.Text = totalAmount.ToString("C", CultureInfo.CurrentCulture);
        }

        // 🔹 NUMERIC KEYPAD LOGIC
        private void AppendDiscount(string value)
        {
            if (txtDiscount.Text == "0") txtDiscount.Text = "";
            txtDiscount.Text += value;
        }

        // 🔹 CLEAR BUTTON
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDiscount.Text = "0";
            RecalculateTotals();
        }

        // 🔹 CONFIRM SALE
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            RecalculateTotals();
            MessageBox.Show("✅ Sale confirmed successfully!", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 🔹 LOGOUT
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Close all open forms and exit the application
                Application.Exit();
            }
        }

        // 🔹 LEFT MENU NAVIGATION
        private void BtnHome_Click(object sender, EventArgs e)
        {
            // Close any open form and show POS interface
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
                activeForm = null;
            }
            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(dgvProductsAvailable);
            panelMainContent.Controls.Add(panel1);
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ProductForm());
        }

        private void BtnUsers_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new UserForm());
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new SalesForm());
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            LoadFormInPanel(new ReportForm());
        }

        // Optional (Header Paint)
        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Optional gradient or shadow effect
        }

        private void dgvProductsAvailable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }

}
