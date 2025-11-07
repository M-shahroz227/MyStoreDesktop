using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        // 🔹 FORM LOAD
        private void Home_Load(object sender, EventArgs e)
        {
            SetupCartGrid();
            SetupEventHandlers();
            RecalculateTotals();
        }

        // 🔹 GRID SETUP
        private void SetupCartGrid()
        {
            dgvCart.Columns.Clear();
            dgvCart.AllowUserToAddRows = true;
            dgvCart.AllowUserToDeleteRows = true;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCart.Columns.Add("ItemName", "Item");
            dgvCart.Columns.Add("Size", "Size");
            dgvCart.Columns.Add("Quantity", "Qty");
            dgvCart.Columns.Add("UnitPrice", "Price");
            dgvCart.Columns.Add("Total", "Total");

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

            dgvCart.Columns.Add(editBtn);
            dgvCart.Columns.Add(delBtn);

            dgvCart.Columns["Quantity"].ValueType = typeof(int);
            dgvCart.Columns["UnitPrice"].ValueType = typeof(decimal);
            dgvCart.Columns["Total"].ReadOnly = true;
        }

        // 🔹 EVENT HANDLERS SETUP
        private void SetupEventHandlers()
        {
            dgvCart.CellValueChanged += (s, e) => RecalculateTotals();
            dgvCart.RowsAdded += (s, e) => RecalculateTotals();
            dgvCart.RowsRemoved += (s, e) => RecalculateTotals();

            dgvCart.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvCart.IsCurrentCellDirty)
                    dgvCart.CommitEdit(DataGridViewDataErrorContexts.Commit);
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

            foreach (DataGridViewRow row in dgvCart.Rows)
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
            MessageBox.Show("🏠 You are already on the Home page.", "Info");
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            var products = new ProductForm();
            products.Show();
            
        }

        private void BtnUsers_Click(object sender, EventArgs e)
        {
            var users = new UserForm();
            users.Show();
            
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            var sales = new SalesForm(); // This will now refer to the real SalesForm
            sales.Show();
            
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            var reports = new ReportForm();
            reports.Show();
            
        }

        // Optional (Header Paint)
        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Optional gradient or shadow effect
        }
    }

}
