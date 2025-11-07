using MyStoreDesktop.Models;
using MyStoreDesktop.Services.QrTableDataService; // ✅ service ka namespace
using MyStoreDesktop.Data;
using QRCoder; // ✅ install from NuGet: Install-Package QRCoder
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class QrForm : Form
    {
        // ✅ Services
        private readonly QrTableDataService _qrService = new QrTableDataService();
        private readonly DatabaseHelper _context = new DatabaseHelper();

        public QrForm()
        {
            InitializeComponent();
        }

        private void QrForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadQrCodes();
        }

        // 🔹 Product List Load
        private void LoadProducts()
        {
            var products = _context.Products
                .Select(p => new { p.ProductId, p.Title })
                .ToList();

            cmbProducts.DataSource = products;
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ProductId";
        }

        // 🔹 QR Codes Load
        private void LoadQrCodes()
        {
            var qrList = _qrService.GetAll()
                .Select(q => new
                {
                    q.Id,
                    Product = q.Product?.Title,
                    q.QrCode,
                    q.CreatedAt
                })
                .ToList();

            dgvQrCodes.DataSource = qrList;
        }

        // 🔹 Generate QR
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedValue == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            string qrText = $"ProductID:{cmbProducts.SelectedValue} - {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            txtQrCode.Text = qrText;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(6);

            picQr.Image = qrCodeImage;
        }

        // 🔹 Save QR Code
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedValue == null || string.IsNullOrWhiteSpace(txtQrCode.Text))
            {
                MessageBox.Show("Please generate a QR code first.");
                return;
            }

            var qr = new QrTableData
            {
                ProductId = Convert.ToInt32(cmbProducts.SelectedValue),
                QrCode = Guid.NewGuid(),
                CreatedAt = DateTime.Now
            };

            _qrService.Add(qr);
            MessageBox.Show("✅ QR Code saved successfully!");
            LoadQrCodes();
            ClearForm();
        }

        // 🔹 Clear Form Controls
        private void ClearForm()
        {
            txtQrCode.Clear();
            picQr.Image = null;
            cmbProducts.SelectedIndex = -1;
        }

        // 🔹 Delete QR Code (optional button)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvQrCodes.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvQrCodes.CurrentRow.Cells["Id"].Value);
                _qrService.Delete(id);
                MessageBox.Show("🗑 QR Code deleted successfully!");
                LoadQrCodes();
            }
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {

        }
    }
}
