using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class BarcodePopup : Form
    {
        private PictureBox picture;
        private Button btnPrint;
        private Button btnAdd;

        public Image BarcodeImage { get; set; }
        public string BarcodeValue { get; internal set; }

        public BarcodePopup(Image barcodeImg)
        {
            BarcodeImage = barcodeImg;

            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // === Form UI ===
            this.Text = "Generated Barcode";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.BackColor = Color.WhiteSmoke;
            this.Padding = new Padding(20);
            this.Width = 420;
            this.Height = 450;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // === PictureBox ===
            picture = new PictureBox
            {
                Image = BarcodeImage,
                Dock = DockStyle.Top,
                Height = 260,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // === Print Button ===
            btnPrint = new Button
            {
                Text = "Print Barcode",
                Width = 130,
                Height = 40,
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPrint.Click += BtnPrint_Click;

            // === Add To Product Button ===
            btnAdd = new Button
            {
                Text = "Add To Product",
                Width = 140,
                Height = 40,
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAdd.Click += BtnAdd_Click;

            // === Buttons Panel ===
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10)
            };
            panel.Controls.Add(btnAdd);
            panel.Controls.Add(btnPrint);

            // === Add Controls to Form ===
            this.Controls.Add(picture);
            this.Controls.Add(panel);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (BarcodeImage == null)
            {
                MessageBox.Show("No Barcode to print!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDoc_PrintPage;

                PrintDialog printDialog = new PrintDialog { Document = printDoc };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                    MessageBox.Show("Barcode sent to printer!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing Barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // high quality
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            Rectangle marginBounds = e.MarginBounds;
            float availableWidth = marginBounds.Width;
            float availableHeight = marginBounds.Height;

            // scale to fit
            float scale = Math.Min(availableWidth / BarcodeImage.Width, availableHeight / BarcodeImage.Height);
            float width = BarcodeImage.Width * scale;
            float height = BarcodeImage.Height * scale;

            float x = marginBounds.Left + (availableWidth - width) / 2f;
            float y = marginBounds.Top + (availableHeight - height) / 2f;

            e.Graphics.FillRectangle(Brushes.White, x, y, width, height);
            e.Graphics.DrawImage(BarcodeImage, x, y, width, height);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
