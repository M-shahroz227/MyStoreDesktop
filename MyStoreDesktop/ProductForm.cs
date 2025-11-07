using System;
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

        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        // 🔹 Load all products into DataGridView
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
                    p.UrlImage
                })
                .ToList();

            dgvProducts.DataSource = products;
        }

        // 🔹 Clear input fields
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
            selectedProductId = 0;
            selectedImagePath = "";
        }

        // 🔹 Add Product + Generate QR
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
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
                GenerateAndSaveQrCode(product); // update QR too

                MessageBox.Show("✅ Product updated successfully!");
                LoadProducts();
                ClearForm();
            }
        }

        // 🔹 Generate QR Code and Save in Database
        private void GenerateAndSaveQrCode(Product product)
        {
            string qrText = $"Product ID: {product.ProductId}\n" +
                            $"Name: {product.Title}\n" +
                            $"Price: {product.SalePrice:C}\n" +
                            $"Company: {product.Company}";

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
                txtCategory.Text = row.Cells["Category"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtSalePrice.Text = row.Cells["SalePrice"].Value.ToString();
                txtPurchasePrice.Text = row.Cells["PurchasePrice"].Value.ToString();
                txtDiscount.Text = row.Cells["Discount"].Value.ToString();
                txtCompany.Text = row.Cells["Company"].Value.ToString();
                txtModel.Text = row.Cells["Model"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
            }
        }
    }
}
