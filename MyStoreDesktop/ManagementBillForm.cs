using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services;
using MyStoreDesktop.Services.UserService;
using MyStoreDesktop.Theme;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop.Forms
{
    public partial class ManagementBillForm : Form
    {
        private readonly DatabaseHelper _context =new DatabaseHelper();
        private readonly IReturnService _returnService = new ReturnService();
        private readonly IUserService _userService = new UserService();



        public ManagementBillForm()
        {

            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvBillProducts.AutoGenerateColumns = false;
            dgvBillProducts.ColumnHeadersHeight = 40;  // height in pixels
            dgvBillProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;


            dgvBillProducts.Columns.Clear();

            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "BillProductId",
                DataPropertyName = "BillProductId",
                Name = "BillProductId",
                Visible = false
            });

            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product",
                DataPropertyName = "ProductName",
                ReadOnly = true
            });

            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qty",
                DataPropertyName = "Quantity",
                ReadOnly = true
            });

            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                DataPropertyName = "Price",
                ReadOnly = true
            });

            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DataPropertyName = "Amount",
                ReadOnly = true
            });

            dgvBillProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Returned",
                DataPropertyName = "IsReturn",
                ReadOnly = true
            });

            var returnButton = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Return",
                Name = "ReturnButtonColumn",
                UseColumnTextForButtonValue = true
            };
            dgvBillProducts.Columns.Add(returnButton);

            dgvBillProducts.CellContentClick += DgvBillProducts_CellContentClick;
            var modifyButton = new DataGridViewButtonColumn
            {
                HeaderText = "Action",
                Text = "Modify",
                Name = "ModifyButtonColumn",
                UseColumnTextForButtonValue = true
            };
            dgvBillProducts.Columns.Add(modifyButton);
        }

        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            int billId;
            if (!int.TryParse(txtBillID.Text, out billId))
            {
                MessageBox.Show("Enter valid Bill ID");
                return;
            }

            var bill = _context.Bills
                        .Where(b => b.BillId == billId)
                        .FirstOrDefault();

            if (bill == null)
            {
                MessageBox.Show("Bill not found");
                return;
            }

            dgvBillProducts.DataSource = bill.BillProducts
                .Select(bp => new
                {
                    bp.BillProductId,
                    ProductName = bp.Product.Title,
                    bp.Quantity,
                    Price = bp.ItemPrice,
                    Amount = bp.TotalPrice,
                    bp.IsReturn
                })
                .ToList();

            lblGrandTotal.Text = $"Grand Total: {bill.GrandTotal}";
        }

        private void DgvBillProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int billId = int.Parse(txtBillID.Text);
            string currentUser = SessionManager.UserName;

            // ---------------- Return Button ----------------
            if (dgvBillProducts.Columns[e.ColumnIndex].Name == "ReturnButtonColumn")
            {
                int billProductId = Convert.ToInt32(
                    dgvBillProducts.Rows[e.RowIndex].Cells["BillProductId"].Value);

                _returnService.ReturnProduct(billId, billProductId, currentUser);

                MessageBox.Show("Product returned successfully!");
                btnSearchBill_Click(null, null); // Refresh grid
            }
            // ---------------- Modify Button ----------------
            else if (dgvBillProducts.Columns[e.ColumnIndex].Name == "ModifyButtonColumn")
            {
                int billProductId = Convert.ToInt32(
                    dgvBillProducts.Rows[e.RowIndex].Cells["BillProductId"].Value);

                // Open ModifyReturnForm
                var modifyForm = new ModifyReturnForm(billId, billProductId, (ReturnService)_returnService, currentUser);
                modifyForm.ShowDialog(); // modal dialog

                // Refresh grid after modification
                btnSearchBill_Click(null, null);
            }
        }

        private void btnViewReturnHistoryForm_Click(object sender, EventArgs e)
        {
            var viewhistryform = new ViewReturnHistoryForm();
            viewhistryform.Show();
        }
    }
}

