using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class Home : Form
    {
        private readonly ProductService _productService = new ProductService();
        
        
        

        // Value holders for totals
        private double _subtotal = 0;
        private double _discountPercent = 0; // Example 10%
        private double _taxPercent = 0;       // Example 5%
        public Home()
        {
            InitializeComponent();
            lstSuggestion.Visible = false;

            // Setup grid when form loads
            
            SetupGridButtons();

            // Set default colors
            dgvAddToCard.DefaultCellStyle.ForeColor = Color.Black;
            dgvAddToCard.DefaultCellStyle.BackColor = Color.White;
            dgvAddToCard.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // Attach event for button clicks in grid
            dgvAddToCard.CellContentClick += dgvAddToCard_CellContentClick;
        }

        // ======================== GRID SETUP ========================
        

        private void SetupGridButtons()
        {
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "Delete";
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.DefaultCellStyle.SelectionBackColor= Color.IndianRed;
            deleteButton.UseColumnTextForButtonValue = true;
            dgvAddToCard.Columns.Add(deleteButton);
        }
        private void lblTaxValue_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                lstSuggestion.Visible = false;
                return;
            }

            var products = _productService.GetAll()
                .Where(p =>
                    p.Title.ToLower().Contains(search) ||
                    (p.Model != null && p.Model.ToLower().Contains(search)) ||
                    (p.Category != null && p.Category.Title.ToLower().Contains(search)) ||
                    (p.ProductCode != null && p.ProductCode.ToLower().Equals(search)) ||
                    (p.Company != null && p.Company.Title.ToLower().Contains(search)))
                .ToList(); // full Product list

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
                    row.Cells["Total"].Value = newQty * product.SalePrice;

                    UpdateTotals();
                    return;
                }
            }

            double total = (double)(product.SalePrice * 1);
            dgvAddToCard.Rows.Add(product.ProductId, product.Title, 1, product.SalePrice,product.Discount, total);

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
        private void dgvAddToCard_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvAddToCard.Columns["Quantity"].Index)
            {
                UpdateRowTotal(e.RowIndex);
            }

        }
        private void UpdateRowTotal(int RowIndex)
        {
             DataGridViewRow row = dgvAddToCard.Rows[RowIndex];
            double price = Convert.ToDouble(row.Cells["SalePrice"].Value);
            double qunatity = Convert.ToDouble(row.Cells["Quantity"].Value);
            double total = price * qunatity;
            row.Cells["Total"].Value = total;
            UpdateTotals();
        }

        private void dgvAddToCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string columnName = dgvAddToCard.Columns[e.ColumnIndex].Name;

             if (columnName == "Delete")
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this item?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    dgvAddToCard.Rows.RemoveAt(e.RowIndex);
                    UpdateTotals();
                }
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            txtSearch.Text += btn.Text;
           
        }
        private void BillConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bill comformed");
            

        }
        private void LoginPanelbtnHome(object sender, EventArgs e)
        {
            var home = new Home();
            home.Show();
            this.Hide();
        }

        private void LoginPanelProduct(object sender, EventArgs e)
        {
            var product = new ProductForm();
            product.Show();
            this.Hide();
        }

        private void LoginPanelUsers(object sender, EventArgs e)
        {
            var user = new UserForm();
            user.Show();
            this.Hide();
        }

        private void LoginPanelSales(object sender, EventArgs e)
        {
            var sales = new SalesForm();
            sales.Show();
            this.Hide();
        }

        private void LoginPanelReports(object sender, EventArgs e)
        {
            var report = new ReportForm();
            report.Show();
            this.Hide();
        }
        private void txtTaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                double taxPercent;
                if(double.TryParse(txtTaxValue.Text,out taxPercent))
                {
                    _taxPercent = taxPercent;
                }
                else
                {
                    _taxPercent= 0;
                }
                UpdateTotals();
                e.SuppressKeyPress = true;
            }
        }

        private void lblDiscountValue_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                double discountPercent;
                if(double.TryParse(lblDiscountValue.Text,out discountPercent))
                {
                    _discountPercent = discountPercent;
                }
                else { 
                    _discountPercent= 0;
                }
                UpdateTotals();
                e.SuppressKeyPress = true;
            }
        }
    }

}

