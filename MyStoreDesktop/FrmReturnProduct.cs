using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace POSApp
{
    public partial class FrmReturnProduct : Form
    {
        private Label lblBill;
        private TextBox txtBillID;
        private Button btnSearch;
        private DataGridView dgvPurchasedItems;
        private Button btnReturn;
        private Label lblStatus;

        private readonly IReturnService _returnService;

        public FrmReturnProduct()
        {
            _returnService = new ReturnService();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Return Product";
            this.Width = 850;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblBill = new Label { Text = "Enter Bill ID:", Left = 20, Top = 20, Width = 120 };
            txtBillID = new TextBox { Left = 150, Top = 18, Width = 200 };
            btnSearch = new Button { Text = "Search", Left = 370, Top = 16, Width = 100 };
            btnSearch.Click += BtnSearch_Click;

            dgvPurchasedItems = new DataGridView
            {
                Left = 20,
                Top = 60,
                Width = 780,
                Height = 330,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                ReadOnly = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            btnReturn = new Button { Text = "Return Selected Item(s)", Left = 20, Top = 400, Width = 200 };
            btnReturn.Click += BtnReturn_Click;

            lblStatus = new Label { Left = 240, Top = 405, Width = 560, ForeColor = System.Drawing.Color.Blue };

            this.Controls.Add(lblBill);
            this.Controls.Add(txtBillID);
            this.Controls.Add(btnSearch);
            this.Controls.Add(dgvPurchasedItems);
            this.Controls.Add(btnReturn);
            this.Controls.Add(lblStatus);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string billIdText = txtBillID.Text.Trim();
            if (!int.TryParse(billIdText, out int billId))
            {
                MessageBox.Show("Enter valid Bill ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new DatabaseHelper())
                {
                    var items = context.BillProducts
                        .Where(b => b.BillId == billId)
                        .Select(b => new
                        {
                            b.BillProductId,
                            b.ProductId,
                            b.Title,
                            QuantityPurchased = b.Quantity,
                            UnitPrice = b.ItemPrice,  // use ItemPrice from BillProduct
                            Total = b.TotalPrice,
                            ReturnQuantity = 0
                        })
                        .ToList();

                    dgvPurchasedItems.DataSource = items;
                    lblStatus.Text = $"{items.Count} item(s) found for Bill {billId}.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching bill: {ex.Message}");
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (dgvPurchasedItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select item(s) to return.");
                return;
            }

            try
            {
                var returnInvoice = new Return
                {
                    BillId = int.Parse(txtBillID.Text.Trim()),
                    ReturnDate = DateTime.Now
                };
                int returnId = _returnService.CreateReturn(returnInvoice);

                foreach (DataGridViewRow row in dgvPurchasedItems.SelectedRows)
                {
                    int billProductId = Convert.ToInt32(row.Cells["BillProductId"].Value);
                    int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                    int returnQty = Convert.ToInt32(row.Cells["ReturnQuantity"].Value);

                    if (returnQty <= 0) continue;

                    var returnItem = new ReturnItem
                    {
                        ReturnId = returnId,
                        BillProductId = billProductId,
                        ProductId = productId,
                        ReturnQuantity = returnQty
                    };

                    _returnService.AddReturnItem(returnItem);
                }

                MessageBox.Show("Return processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
