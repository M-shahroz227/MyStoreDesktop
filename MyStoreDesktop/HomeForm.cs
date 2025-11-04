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
            SetupRecalculateHandlers();
        }

        // 🔹 Calculate subtotal, discount, tax, total
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

            // Discount
            decimal discountPercent = 0m;
            decimal.TryParse(txtDiscount.Text, out discountPercent);
            decimal discountAmount = subtotal * (discountPercent / 100m);
            decimal afterDiscount = subtotal - discountAmount;

            // Tax (10%)
            decimal tax = afterDiscount * 0.10m;

            // Total
            decimal totalAmount = afterDiscount + tax;

            lblSubTotalValue.Text = subtotal.ToString("C", CultureInfo.CurrentCulture);
            lblTaxValue.Text = tax.ToString("C", CultureInfo.CurrentCulture);
            lblTotalValue.Text = totalAmount.ToString("C", CultureInfo.CurrentCulture);
        }

        // 🔹 Recalculate handlers
        private void SetupRecalculateHandlers()
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
        }

        // 🔹 Home Form Load
        private void Home_Load(object sender, EventArgs e)
        {
            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("ItemName", "Item");
            dgvCart.Columns.Add("Size", "Size");
            dgvCart.Columns.Add("Quantity", "Qty");
            dgvCart.Columns.Add("UnitPrice", "Price");
            dgvCart.Columns.Add("Total", "Total");

            var editBtn = new DataGridViewButtonColumn()
            {
                Name = "Edit",
                Text = "✎",
                UseColumnTextForButtonValue = true
            };

            var delBtn = new DataGridViewButtonColumn()
            {
                Name = "Delete",
                Text = "✖",
                UseColumnTextForButtonValue = true
            };

            dgvCart.Columns.Add(editBtn);
            dgvCart.Columns.Add(delBtn);

            dgvCart.Columns["Quantity"].ValueType = typeof(int);
            dgvCart.Columns["UnitPrice"].ValueType = typeof(decimal);
            dgvCart.Columns["Total"].ReadOnly = true;

            dgvCart.AllowUserToAddRows = true;
            dgvCart.AllowUserToDeleteRows = true;

            RecalculateTotals();
        }

        // 🔹 Numeric keypad click
        private void NumButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
                txtDiscount.Text += btn.Text;
        }

        // 🔹 Clear keypad
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDiscount.Clear();
        }

        // 🔹 Confirm Sale
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            RecalculateTotals();
            MessageBox.Show("✅ Sale Confirmed Successfully!", "POS System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
