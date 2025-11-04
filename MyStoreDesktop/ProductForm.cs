using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            dgvProducts.Columns.Add("ProductName", "Name");
            dgvProducts.Columns.Add("Category", "Category");
            dgvProducts.Columns.Add("Price", "Price");
            dgvProducts.Columns.Add("Stock", "Stock");

            var editBtn = new DataGridViewButtonColumn()
            {
                Name = "Edit",
                Text = "✎",
                UseColumnTextForButtonValue = true
            };
            var delBtn = new DataGridViewButtonColumn()
            {
                Name = "Delete",
                Text = "🗑",
                UseColumnTextForButtonValue = true
            };

            dgvProducts.Columns.Add(editBtn);
            dgvProducts.Columns.Add(delBtn);

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvProducts.Rows.Add(txtName.Text, cmbCategory.Text, txtPrice.Text, txtStock.Text);
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbCategory.SelectedIndex = -1;
        }

    }
}
