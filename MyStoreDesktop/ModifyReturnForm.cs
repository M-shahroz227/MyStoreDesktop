using System;
using System.Drawing;
using System.Windows.Forms;
using MyStoreDesktop.Services;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Forms
{
    public partial class ModifyReturnForm : Form
    {
        private readonly int _billId;
        private readonly int _billProductId;
        private readonly ReturnService _returnService;
        private readonly string _currentUser;

        // Controls
        private Label lblQuantity;
        private Label lblPrice;
        private NumericUpDown numericQuantity;
        private NumericUpDown numericPrice;
        private Button btnSave;
        private Button btnCancel;

        public ModifyReturnForm(int billId, int billProductId, ReturnService returnService, string currentUser)
        {
            _billId = billId;
            _billProductId = billProductId;
            _returnService = returnService;
            _currentUser = currentUser;

            // Form properties
            this.Text = "Modify Returned Product";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(320, 200);
            this.BackColor = Color.White;

            InitializeControls();

            // Load the current values from the bill product
            LoadCurrentValues();
        }

        private void InitializeControls()
        {
            // Labels
            lblQuantity = new Label()
            {
                Text = "Quantity:",
                Left = 20,
                Top = 30,
                Width = 120,
                Font = new Font("Segoe UI", 10)
            };

            lblPrice = new Label()
            {
                Text = "Price:",
                Left = 20,
                Top = 80,
                Width = 120,
                Font = new Font("Segoe UI", 10)
            };

            // NumericUpDowns
            numericQuantity = new NumericUpDown()
            {
                Left = 150,
                Top = 28,
                Width = 120,
                Minimum = 1,
                Maximum = 1000,
                Font = new Font("Segoe UI", 10)
            };

            numericPrice = new NumericUpDown()
            {
                Left = 150,
                Top = 78,
                Width = 120,
                Minimum = 0,
                Maximum = 100000,
                DecimalPlaces = 2,
                Increment = 0.5M,
                Font = new Font("Segoe UI", 10)
            };

            // Buttons
            btnSave = new Button()
            {
                Text = "Save",
                Left = 50,
                Top = 140,
                Width = 90,
                Height = 35,
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button()
            {
                Text = "Cancel",
                Left = 180,
                Top = 140,
                Width = 90,
                Height = 35,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            // Add to form
            this.Controls.Add(lblQuantity);
            this.Controls.Add(numericQuantity);
            this.Controls.Add(lblPrice);
            this.Controls.Add(numericPrice);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void LoadCurrentValues()
        {
            // Get the current BillProduct from ReturnService
            BillProduct bp = _returnService.GetBillProduct(_billId, _billProductId);
            if (bp != null)
            {
                numericQuantity.Value = bp.Quantity;
                numericPrice.Value = bp.ItemPrice;

                // Optional: set max to prevent crazy values
                numericQuantity.Maximum = bp.Quantity * 2;
                numericPrice.Maximum = bp.ItemPrice * 5;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int newQty = (int)numericQuantity.Value;
            decimal newPrice = numericPrice.Value;

            try
            {
                _returnService.ModifyReturnedProduct(_billId, _billProductId, newQty, newPrice, _currentUser);
                MessageBox.Show("Product modified successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
