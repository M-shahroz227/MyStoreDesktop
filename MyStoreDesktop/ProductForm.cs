using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;
using MyStoreDesktop.Services.QrTableDataService;
using QRCoder;
using System.Drawing;
using System.IO;

namespace MyStoreDesktop
{
    public partial class ProductForm : Form
    {
        private readonly IProductService _productService = new ProductService();
        private readonly IQrTableDataService _qrService = new QrTableDataService();
        private int selectedProductId = 0;
        private string selectedImagePath = "";
        private Bitmap currentQRCode = null;
        private string currentQRCodeGuid = "";

        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadCompanies();
            LoadProducts();
        }

        // ?? Load all products into DataGridView
        private void LoadProducts()
        {
            var products = _productService.GetAll()
                .Select(p => new
                {
                    p.ProductId,
                    p.Title,
                    Category = p.Category != null ? p.Category.Title : string.Empty,
                    p.CategoryId,
                    Company = p.Company != null ? p.Company.Title : string.Empty,
                    p.CompanyId,
                    p.Quantity,
                    p.SalePrice,
                    p.PurchasePrice,
                    p.Discount,
                    p.Model,
                    p.Description,
                    p.UrlImage
                })
                .ToList();

            dgvProducts.DataSource = products;

            if (dgvProducts.Columns.Contains("CategoryId"))
            {
                dgvProducts.Columns["CategoryId"].Visible = false;
            }
            if (dgvProducts.Columns.Contains("CompanyId"))
            {
                dgvProducts.Columns["CompanyId"].Visible = false;
            }
        }

        private void LoadCategories(int? categoryIdToSelect = null)
        {
            var categories = _productService.GetCategories().ToList();

            cboCategory.DataSource = null;
            cboCategory.Items.Clear();

            if (categories.Any())
            {
                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = nameof(Category.Title);
                cboCategory.ValueMember = nameof(Category.CategoryId);
                cboCategory.Enabled = true;

                if (categoryIdToSelect.HasValue && categories.Any(c => c.CategoryId == categoryIdToSelect.Value))
                {
                    cboCategory.SelectedValue = categoryIdToSelect.Value;
                }
                else
                {
                    cboCategory.SelectedIndex = 0;
                }
            }
            else
            {
                cboCategory.Enabled = false;
                cboCategory.SelectedIndex = -1;
                cboCategory.Text = string.Empty;
            }
        }

        private void LoadCompanies(int? companyIdToSelect = null)
        {
            var companies = _productService.GetCompanies().ToList();

            cboCompany.DataSource = null;
            cboCompany.Items.Clear();

            if (companies.Any())
            {
                cboCompany.DataSource = companies;
                cboCompany.DisplayMember = nameof(Company.Title);
                cboCompany.ValueMember = nameof(Company.CompanyId);
                cboCompany.Enabled = true;

                if (companyIdToSelect.HasValue && companies.Any(c => c.CompanyId == companyIdToSelect.Value))
                {
                    cboCompany.SelectedValue = companyIdToSelect.Value;
                }
                else
                {
                    cboCompany.SelectedIndex = 0;
                }
            }
            else
            {
                cboCompany.Enabled = false;
                cboCompany.SelectedIndex = -1;
                cboCompany.Text = string.Empty;
            }
        }

        // 🔹 Clear input fields
        private void ClearForm()
        {
            txtTitle.Clear();
            txtQuantity.Clear();
            txtSalePrice.Clear();
            txtPurchasePrice.Clear();
            txtDiscount.Clear();
            txtModel.Clear();
            txtDescription.Clear();
            selectedProductId = 0;
            selectedImagePath = "";
            
            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex = 0;
            }
            else
            {
                cboCategory.SelectedIndex = -1;
            }

            if (cboCompany.Items.Count > 0)
            {
                cboCompany.SelectedIndex = 0;
            }
            else
            {
                cboCompany.SelectedIndex = -1;
            }
        }

        // 🔹 Add Product + Generate QR
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Please select a category before adding a product.");
                    return;
                }

                if (cboCompany.SelectedValue == null)
                {
                    MessageBox.Show("Please select a company before adding a product.");
                    return;
                }

                int categoryId = (int)cboCategory.SelectedValue;
                int companyId = (int)cboCompany.SelectedValue;

                var product = new Product
                {
                    Title = txtTitle.Text,
                    CategoryId = categoryId,
                    CompanyId = companyId,
                    Quantity = int.Parse(txtQuantity.Text),
                    SalePrice = decimal.Parse(txtSalePrice.Text),
                    PurchasePrice = decimal.Parse(txtPurchasePrice.Text),
                    Discount = decimal.Parse(txtDiscount.Text),
                    Model = txtModel.Text,
                    Description = txtDescription.Text,
                    UrlImage = selectedImagePath
                };

                // ✅ Add Product (returns product with ProductId)
                var addedProduct = _productService.Add(product);

                // ✅ Generate QR for new product
                GenerateAndSaveQrCode(addedProduct);

                MessageBox.Show("✅ Product added successfully!");
                LoadProducts();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error adding product: " + ex.Message);
            }
        }

        // 🔹 Update Product + Regenerate QR
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("⚠️ Please select a product to update.");
                return;
            }

            var product = _productService.GetById(selectedProductId);
            if (product != null)
            {
                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Please select a category before updating the product.");
                    return;
                }

                if (cboCompany.SelectedValue == null)
                {
                    MessageBox.Show("Please select a company before updating the product.");
                    return;
                }

                int categoryId = (int)cboCategory.SelectedValue;
                int companyId = (int)cboCompany.SelectedValue;

                product.Title = txtTitle.Text;
                product.CategoryId = categoryId;
                product.CompanyId = companyId;
                product.Quantity = int.Parse(txtQuantity.Text);
                product.SalePrice = decimal.Parse(txtSalePrice.Text);
                product.PurchasePrice = decimal.Parse(txtPurchasePrice.Text);
                product.Discount = decimal.Parse(txtDiscount.Text);
                product.Model = txtModel.Text;
                product.Description = txtDescription.Text;
                product.UrlImage = selectedImagePath;

                _productService.Update(product);
                GenerateAndSaveQrCode(product); // update QR too

                MessageBox.Show("✅ Product updated successfully!");
                LoadProducts();
                ClearForm();
            }
        }

        // 🔹 Delete Product
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("⚠️ Please select a product to delete.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    _productService.Delete(selectedProductId);
                    MessageBox.Show("✅ Product deleted successfully!");
                    LoadProducts();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error deleting product: " + ex.Message);
                }
            }
        }

        // 🔹 Generate QR Code and Save in Database
        private void GenerateAndSaveQrCode(Product product)
        {
            string qrText = $"Product ID: {product.ProductId}\n" +
                            $"Name: {product.Title}\n" +
                            $"Price: {product.SalePrice:C}\n" +
                            $"Company: {(product.Company != null ? product.Company.Title : "N/A")}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrData))
                {
                    Bitmap qrImage = qrCode.GetGraphic(20);

                    // 🔹 Save QR image to folder
                    string folderPath = Path.Combine(Application.StartupPath, "QRCodes");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string qrFilePath = Path.Combine(folderPath, $"QR_{product.ProductId}.png");
                    qrImage.Save(qrFilePath);

                    // 🔹 Save in database
                    var qrDataModel = new QrTableData
                    {
                        ProductId = product.ProductId,
                        QrCode = Guid.NewGuid(),
                        CreatedAt = DateTime.Now
                    };

                    _qrService.Add(qrDataModel);
                }
            }
        }

        // 🔹 When product row is selected
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);
                txtTitle.Text = row.Cells["Title"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtSalePrice.Text = row.Cells["SalePrice"].Value.ToString();
                txtPurchasePrice.Text = row.Cells["PurchasePrice"].Value.ToString();
                txtDiscount.Text = row.Cells["Discount"].Value.ToString();
                txtModel.Text = row.Cells["Model"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();

                if (row.Cells["CategoryId"].Value != null && cboCategory.DataSource is IEnumerable<Category> categoryList)
                {
                    int categoryId = Convert.ToInt32(row.Cells["CategoryId"].Value);
                    if (categoryList.Any(c => c.CategoryId == categoryId))
                    {
                        cboCategory.SelectedValue = categoryId;
                    }
                }

                if (row.Cells["CompanyId"].Value != null && cboCompany.DataSource is IEnumerable<Company> companyList)
                {
                    int companyId = Convert.ToInt32(row.Cells["CompanyId"].Value);
                    if (companyList.Any(c => c.CompanyId == companyId))
                    {
                        cboCompany.SelectedValue = companyId;
                    }
                }
            }
        }

        private void btnManageCategories_Click(object sender, EventArgs e)
        {
            int? previousCategoryId = cboCategory.SelectedValue is int value ? value : (int?)null;

            using (var categoryManager = new CategoryManagerForm(_productService))
            {
                var result = categoryManager.ShowDialog(this);
                int? categoryToSelect = result == DialogResult.OK ? categoryManager.SelectedCategoryId : previousCategoryId;
                LoadCategories(categoryToSelect);
                LoadProducts();
            }
        }

        private void btnManageCompanies_Click(object sender, EventArgs e)
        {
            int? previousCompanyId = cboCompany.SelectedValue is int value ? value : (int?)null;

            using (var companyManager = new CompanyManagerForm(_productService))
            {
                var result = companyManager.ShowDialog(this);
                int? companyToSelect = result == DialogResult.OK ? companyManager.SelectedCompanyId : previousCompanyId;
                LoadCompanies(companyToSelect);
                LoadProducts();
            }
        }

        // 🔹 Generate QR Code Button Click
        private void btnGenerateQR_Click(object sender, EventArgs e)
        {
            try
            {
                // Generate new GUID for QR code
                currentQRCodeGuid = Guid.NewGuid().ToString();

                // Generate QR Code using QRCoder
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(currentQRCodeGuid, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Create high-quality bitmap for printing
                currentQRCode = qrCode.GetGraphic(20); // Higher pixels per module for better print quality

                // Show popup dialog
                QRCodePopup popup = new QRCodePopup();
                popup.QRCodeImage = currentQRCode;
                popup.QRCodeGuid = currentQRCodeGuid;

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    // Display QR code in the preview picture box
                    picQRPreview.Image = currentQRCode;
                    MessageBox.Show($"QR Code added to product!\nGUID: {currentQRCodeGuid}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR Code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
