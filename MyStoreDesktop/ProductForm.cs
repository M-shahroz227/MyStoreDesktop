using MyStoreDesktop.Models;
using MyStoreDesktop.Services.FileServices;
using MyStoreDesktop.Services.ProductService;
using MyStoreDesktop.Services.QrTableDataService;
using MyStoreDesktop.Theme;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Text.RegularExpressions;



namespace MyStoreDesktop
{
    public partial class ProductForm : Form
    {
        private readonly IProductService _productService = new ProductService();
        private readonly IQrTableDataService _qrService = new QrTableDataService();
        private readonly FileServices _fileServices = new FileServices();
        private readonly SettingService _settingService = new SettingService();
        private int selectedProductId = 0;
        private Bitmap currentQRCode = null;
        private string currentQRCodeGuid = "";
        private string _selectedImagePath = "";  // ✅
        

        public ProductForm()
        {
            InitializeComponent();

            // Apply professional blue theme
            ThemeManager.ApplyTheme(this);
           


            cmbCodeType.SelectedIndexChanged += cmbCodeType_SelectedIndexChanged;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadCompanies();
            LoadProducts();
            cmbCodeType.SelectedIndex = 0;
        }

        // Load all products into DataGridView
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

        // Clear input fields
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
            _selectedImagePath = "";
            picProduct.Image = null;
            picQRPreview.Image = null;
            picBarcodePreview.Image = null;
            
            txtManualCode.Text = "";

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

        // Add Product + Generate QR
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
                if (!ValidateForm())
                {
                    MessageBox.Show("Please fix validation errors", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


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
                    UrlImage = _selectedImagePath

                };
                try { 
                // Add Product (returns product with ProductId)
                var addedProduct = _productService.Add(product);
                     GenerateAndSaveQrCode(addedProduct);

                MessageBox.Show("✅ Product added successfully!");
                LoadProducts();
                ClearForm();
                }catch (Exception ex)
                { MessageBox.Show(ex.Message); return; }

                // Generate QR for new product (this will generate GUID, QR image and save CodeValue)
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error adding product: " + ex.Message);
            }
        }

        // Update Product + Regenerate QR
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

                if (!ValidateForm())
                {
                    MessageBox.Show("Please fix validation errors", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                product.Title = txtTitle.Text;
                product.CategoryId = categoryId;
                product.CompanyId = companyId;
                product.Quantity = int.Parse(txtQuantity.Text);
                product.SalePrice = decimal.Parse(txtSalePrice.Text);
                product.PurchasePrice = decimal.Parse(txtPurchasePrice.Text);
                product.Discount = decimal.Parse(txtDiscount.Text);
                product.Model = txtModel.Text;
                product.Description = txtDescription.Text;
                product.UrlImage = _selectedImagePath;



                try
                {
                    _productService.Update(product);
                    GenerateAndSaveQrCode(product);
                    MessageBox.Show("✅ Product updated successfully!");
                    LoadProducts();
                    ClearForm();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error updating product: " + ex.Message);
                }

            }
        }

        // Delete Product
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

        // Generate QR Code, save image file and save CodeValue (GUID) in DB
        private void GenerateAndSaveQrCode(Product product)
        {
            // create a GUID which we will use as CodeValue and encode into QR
            string qrGuid = Guid.NewGuid().ToString();

            // create human-friendly text if you want to encode some product details instead,
            // but we will encode the GUID to be consistent with CodeValue stored in DB.
            // If you prefer to encode product details, change the code and save CodeValue accordingly.
            string textToEncode = qrGuid;

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrData))
                {
                    Bitmap qrImage = qrCode.GetGraphic(20);

                    // Save QR image to folder
                    string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "QRCodes");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string qrFilePath = Path.Combine(folderPath, $"QR_{product.ProductId}_{DateTime.Now:yyyyMMddHHmmss}.png");
                    qrImage.Save(qrFilePath);

                    // Save CodeValue (GUID) and CodeType = "QR" in DB
                    SaveQRCode(product.ProductId, qrGuid);
                }
            }
        }

        // When product row is selected
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadProductFromRow(dgvProducts.Rows[e.RowIndex]);

            }
        }
        private void LoadProductFromRow(DataGridViewRow row)
        {
            if (row == null)
                return;
            selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);

            txtTitle.Text = row.Cells["Title"].Value.ToString();
            txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
            txtSalePrice.Text = row.Cells["SalePrice"].Value.ToString();
            txtPurchasePrice.Text = row.Cells["PurchasePrice"].Value.ToString();
            txtDiscount.Text = row.Cells["Discount"].Value.ToString();
            txtModel.Text = row.Cells["Model"].Value?.ToString() ?? "";
            txtDescription.Text = row.Cells["Description"].Value?.ToString() ?? "";

            // ✅ CATEGORY LOAD
            if (row.Cells["CategoryId"].Value != null)
                cboCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);

            // ✅ COMPANY LOAD
            if (row.Cells["CompanyId"].Value != null)
                cboCompany.SelectedValue = Convert.ToInt32(row.Cells["CompanyId"].Value);

            // ✅ ✅ IMAGE LOAD (MOST IMPORTANT)
            var cellValue = row.Cells["UrlImage"].Value;

            if (cellValue != null)
            {
                string imgNameWithExt = cellValue.ToString();
                var img = _fileServices.GetFileByName(imgNameWithExt);

                if (img != null)
                {
                    picProduct.Image?.Dispose();
                    picProduct.Image = img;
                    _selectedImagePath = imgNameWithExt; // ✅ IMPORTANT
                }
                else
                {
                    picProduct.Image = null;
                    _selectedImagePath = "";
                }
            }
            else
            {
                picProduct.Image = null;
                _selectedImagePath = "";
            }


        }
        private void dgvProducts_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (dgvProducts.CurrentRow != null)
                {
                    LoadProductFromRow(dgvProducts.CurrentRow);
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

        // Generate QR Code Button Click (preview + optional save if a product is selected)
        private void btnGenerateQR_Click(object sender, EventArgs e)
        {
            try
            {
                // Generate new GUID for QR code preview
                currentQRCodeGuid = Guid.NewGuid().ToString();

                // Generate QR Code using QRCoder
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(currentQRCodeGuid, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Create high-quality bitmap for preview/printing
                currentQRCode = qrCode.GetGraphic(20); // Higher pixels per module for better print quality

                // Show popup dialog
                QRCodePopup popup = new QRCodePopup();
                popup.QRCodeImage = currentQRCode;
                popup.QRCodeGuid = currentQRCodeGuid;

                if (popup.ShowDialog() == DialogResult.OK)
                {
                    // Display QR code in the preview picture box
                    picQRPreview.Image = currentQRCode;

                    // If a product is selected (existing) save the code; otherwise tell user to save product first
                    if (selectedProductId > 0)
                    {
                        SaveQRCode(selectedProductId, currentQRCodeGuid);
                        MessageBox.Show($"QR Code added to product!\nGUID: {currentQRCodeGuid}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("QR generated for preview. Save product first (Add) to persist QR in database.", "Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR Code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbCodeType.SelectedItem.ToString();

            panelQRCode.Visible = (selected == "QR Code");
            panelBarcode.Visible = (selected == "Bar Code");
            
        }

        // Single fixed barcode generator button
        private void btnGenerateBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                // Generate 12-digit random barcode
                string barcodeValue = Guid.NewGuid().ToString("N").Substring(0, 12);

                var writer = new BarcodeWriterPixelData
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 100,
                        Width = 300,
                        Margin = 2
                    }
                };

                // Generate raw pixels
                var pixelData = writer.Write(barcodeValue);

                // Convert pixels to bitmap
                Bitmap bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly,
                    bitmap.PixelFormat
                );

                Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                bitmap.UnlockBits(bitmapData);

                // ---- OPEN POPUP (QR jaise) ----
                BarcodePopup popup = new BarcodePopup(bitmap); // pass your image here
                popup.BarcodeValue = barcodeValue;  // you can still set BarcodeValue separately
                popup.ShowDialog();


                if (popup.ShowDialog() == DialogResult.OK)
                {
                    // Preview screen me show karo
                    picBarcodePreview.Image = bitmap;

                    // If product selected → save
                    if (selectedProductId > 0)
                    {
                        SaveBarcode(selectedProductId, barcodeValue);
                        MessageBox.Show("Barcode added to product!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Barcode generated for preview. Save the product first to store it in database.",
                                        "Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Barcode Error: " + ex.Message);
            }
        }


        private void btnSaveManualCode_Click(object sender, EventArgs e)
        {
            try
            {
                string manualCode = txtManualCode.Text.Trim();

                if (string.IsNullOrEmpty(manualCode))
                {
                    MessageBox.Show("Enter manual code!");
                    return;
                }

                if (selectedProductId > 0)
                {
                    SaveManualCode(selectedProductId, manualCode);
                    MessageBox.Show("Manual Code Saved!");
                }
                else
                {
                    MessageBox.Show("Enter/Select product first (Add a product) to save manual code.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Manual Code Error: " + ex.Message);
            }
        }

        private void SaveQRCode(int productId, string qrText)
        {
            var data = new QrTableData
            {
                ProductId = productId,
                CodeValue = qrText,
                CodeType = "QR",
                CreatedAt = DateTime.Now
            };

            _qrService.Add(data);
        }

        private void SaveBarcode(int productId, string barcodeValue)
        {
            var data = new QrTableData
            {
                ProductId = productId,
                CodeValue = barcodeValue,
                CodeType = "BARCODE",
                CreatedAt = DateTime.Now
            };

            _qrService.Add(data);
        }

        private void SaveManualCode(int productId, string manualCode)
        {
            var data = new QrTableData
            {
                ProductId = productId,
                CodeValue = manualCode,
                CodeType = "MANUAL",
                CreatedAt = DateTime.Now
            };

            _qrService.Add(data);
        }
        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return; // User cancelled

                string sourcePath = ofd.FileName;

                // ✅ Get base folder from settings
                string imgFolder = _settingService.GetByKey("basePath");
                if (string.IsNullOrEmpty(imgFolder))
                {
                    MessageBox.Show("Base image path not set in settings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Ensure folder exists
                if (!Directory.Exists(imgFolder))
                    Directory.CreateDirectory(imgFolder);

                // ✅ Create unique filename (GUID) + extension
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourcePath);

                // ✅ Full destination path
                string destinationPath = Path.Combine(imgFolder, fileName);

                // ✅ Copy image
                File.Copy(sourcePath, destinationPath, true);

                // ✅ Set _selectedImagePath = filename only (DB me save hoga)
                _selectedImagePath = fileName;
                  
                // ✅ Show image preview in picture box
                picProduct.Image?.Dispose();
                picProduct.Image = System.Drawing.Image.FromFile(destinationPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image selection failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _selectedImagePath = "";
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private bool ValidateForm()
        {
            bool isValid = true;

            errorProvider1.Clear();

            // TITLE
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Title is required");
                isValid = false;
            }

            // QUANTITY
            if (!int.TryParse(txtQuantity.Text, out int qty) || qty <= 0)
            {
                errorProvider1.SetError(txtQuantity, "Enter valid quantity");
                isValid = false;
            }

            // SALE PRICE
            if (!decimal.TryParse(txtSalePrice.Text, out decimal sale) || sale <= 0)
            {
                errorProvider1.SetError(txtSalePrice, "Enter valid sale price");
                isValid = false;
            }

            // PURCHASE PRICE
            if (!decimal.TryParse(txtPurchasePrice.Text, out decimal purchase) || purchase <= 0)
            {
                errorProvider1.SetError(txtPurchasePrice, "Enter valid purchase price");
                isValid = false;
            }

            // DISCOUNT
            if (!decimal.TryParse(txtDiscount.Text, out decimal discount) || discount < 0)
            {
                errorProvider1.SetError(txtDiscount, "Enter valid discount");
                isValid = false;
            }

            // MODEL
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                errorProvider1.SetError(txtModel, "Model is required");
                isValid = false;
            }

            // DESCRIPTION
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                errorProvider1.SetError(txtDescription, "Description is required");
                isValid = false;
            }

            // CATEGORY
            if (cboCategory.SelectedValue == null)
            {
                errorProvider1.SetError(cboCategory, "Select category");
                isValid = false;
            }

            // COMPANY
            if (cboCompany.SelectedValue == null)
            {
                errorProvider1.SetError(cboCompany, "Select company");
                isValid = false;
            }

            return isValid;
        }

    }


}

