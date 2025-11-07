using System;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;

namespace MyStoreDesktop
{
    public partial class ProductForm : Form
    {
        private readonly ProductService _productService = new ProductService();
        private int selectedProductId = 0;
        private string selectedImagePath = ""; // Image path track karne ke liye

        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _productService.GetAll()
                .Select(p => new
                {
                    p.ProductId,
                    p.Title,
                    p.Category,
                    p.Quantity,
                    p.SalePrice,
                    p.PurchasePrice,
                    p.Discount,
                    p.Company,
                    p.Model,
                    p.Description,
                    p.UrlImage // Assuming Product model me ImagePath property hai
                }).ToList();

            dgvProducts.DataSource = products;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                Title = txtTitle.Text,
                Category = txtCategory.Text,
                Quantity = int.Parse(txtQuantity.Text),
                SalePrice = decimal.Parse(txtSalePrice.Text),
                PurchasePrice = decimal.Parse(txtPurchasePrice.Text),
                Discount = decimal.Parse(txtDiscount.Text),
                Company = txtCompany.Text,
                Model = txtModel.Text,
                Description = txtDescription.Text,
                UrlImage = selectedImagePath
            };

            _productService.Add(product);
            MessageBox.Show("✅ Product added successfully!");
            LoadProducts();
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            var product = _productService.GetById(selectedProductId);
            if (product != null)
            {
                product.Title = txtTitle.Text;
                product.Category = txtCategory.Text;
                product.Quantity = int.Parse(txtQuantity.Text);
                product.SalePrice = decimal.Parse(txtSalePrice.Text);
                product.PurchasePrice = decimal.Parse(txtPurchasePrice.Text);
                product.Discount = decimal.Parse(txtDiscount.Text);
                product.Company = txtCompany.Text;
                product.Model = txtModel.Text;
                product.Description = txtDescription.Text;
                product.UrlImage = selectedImagePath;

                _productService.Update(product);
                MessageBox.Show("✅ Product updated successfully!");
                LoadProducts();
                ClearForm();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            _productService.Delete(selectedProductId);
            MessageBox.Show("🗑 Product deleted successfully!");
            LoadProducts();
            ClearForm();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedProductId = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["ProductId"].Value);
                txtTitle.Text = dgvProducts.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                txtCategory.Text = dgvProducts.Rows[e.RowIndex].Cells["Category"].Value.ToString();
                txtQuantity.Text = dgvProducts.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                txtSalePrice.Text = dgvProducts.Rows[e.RowIndex].Cells["SalePrice"].Value.ToString();
                txtPurchasePrice.Text = dgvProducts.Rows[e.RowIndex].Cells["PurchasePrice"].Value.ToString();
                txtDiscount.Text = dgvProducts.Rows[e.RowIndex].Cells["Discount"].Value.ToString();
                txtCompany.Text = dgvProducts.Rows[e.RowIndex].Cells["Company"].Value?.ToString();
                txtModel.Text = dgvProducts.Rows[e.RowIndex].Cells["Model"].Value?.ToString();
                txtDescription.Text = dgvProducts.Rows[e.RowIndex].Cells["Description"].Value?.ToString();

                // Load Image into PictureBox
                selectedImagePath = dgvProducts.Rows[e.RowIndex].Cells["ImagePath"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    UrlImage.ImageLocation = selectedImagePath;
                }
                else
                {
                    UrlImage.Image = null;
                }
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    UrlImage.ImageLocation = selectedImagePath;
                }
            }
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtCategory.Clear();
            txtQuantity.Clear();
            txtSalePrice.Clear();
            txtPurchasePrice.Clear();
            txtDiscount.Clear();
            txtCompany.Clear();
            txtModel.Clear();
            txtDescription.Clear();
            UrlImage.Image = null;
            selectedImagePath = "";
            selectedProductId = 0;
        }
    }
}

