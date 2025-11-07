using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class QRCodePopup : Form
    {
        private Bitmap qrCodeImage;
        private string qrCodeGuid;

        public Bitmap QRCodeImage
        {
            get { return qrCodeImage; }
            set
            {
                qrCodeImage = value;
                picQRCode.Image = value;
            }
        }

        public string QRCodeGuid
        {
            get { return qrCodeGuid; }
            set { qrCodeGuid = value; }
        }

        public QRCodePopup()
        {
            InitializeComponent();
        }

        private void btnPrintQR_Click(object sender, EventArgs e)
        {
            if (qrCodeImage == null)
            {
                MessageBox.Show("No QR Code to print!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDoc_PrintPage;

                // Show print dialog
                PrintDialog printDialog = new PrintDialog();
                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                    MessageBox.Show("QR Code sent to printer!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing QR Code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Set high quality rendering
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            // Printable area (units are 1/100th inch by default for print graphics)
            Rectangle marginBounds = e.MarginBounds;
            float availableWidth = marginBounds.Width;
            float availableHeight = marginBounds.Height;

            // Target QR size: 2 inches = 200 hundredths of an inch
            const float targetSizeHundredths = 200f;

            // If printable area smaller than 2 inches, scale down to fit while keeping square
            float scale = Math.Min(1f, Math.Min(availableWidth / targetSizeHundredths, availableHeight / targetSizeHundredths));
            float qrSize = targetSizeHundredths * scale;

            // Center horizontally, top-aligned within margin bounds
            float x = marginBounds.Left + (availableWidth - qrSize) / 2f;
            float y = marginBounds.Top;

            // Add padding around QR (10 hundredths of inch scaled)
            float padding = 10f * scale;
            e.Graphics.FillRectangle(Brushes.White, x - padding, y - padding, qrSize + padding * 2, qrSize + padding * 2);

            // Draw border
            using (Pen borderPen = new Pen(Color.Black, 2f * scale))
            {
                e.Graphics.DrawRectangle(borderPen, x - padding / 2f, y - padding / 2f, qrSize + padding, qrSize + padding);
            }

            // Draw QR code at exactly 2 x 2 inches (scaled if needed)
            e.Graphics.DrawImage(qrCodeImage, x, y, qrSize, qrSize);
        }

        private void btnAddToProduct_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
