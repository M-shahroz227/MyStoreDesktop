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
        private readonly DatabaseHelper _context = new DatabaseHelper();
        private readonly IReturnService _returnService = new ReturnService();
        private readonly IUserService _userService = new UserService();

        public ManagementBillForm()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            SetupDataGridView();

            this.Load += ManagementBillForm_Load;   // Form Load Event
            btnSearchBill.Click += btnSearchBill_Click;
            btnViewReturnHistoryForm.Click += btnViewReturnHistoryForm_Click;
        }

        private void ManagementBillForm_Load(object sender, EventArgs e)
        {
            LoadAllSales();   // Auto-load all bill products
        }

        private void SetupDataGridView()
        {
            dgvBillProducts.AutoGenerateColumns = false;
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
                HeaderText = "BillId",
                DataPropertyName = "BillId",
                Name = "BillId",
                Visible = true
            });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product",
                DataPropertyName = "ProductName"
            });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qty",
                DataPropertyName = "Quantity"
            });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                DataPropertyName = "Price"
            });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DataPropertyName = "Amount"
            });
            dgvBillProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Returned",
                DataPropertyName = "IsReturn",
                Name = "IsReturn"
            });

            dgvBillProducts.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ReturnButtonColumn",
                HeaderText = "Action",
                Text = "Return",
                UseColumnTextForButtonValue = true
            });
            dgvBillProducts.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ModifyButtonColumn",
                HeaderText = "Modify",
                Text = "Modify",
                UseColumnTextForButtonValue = true
            });

            dgvBillProducts.CellContentClick += DgvBillProducts_CellContentClick;
        }

        private void LoadAllSales()
        {
            var allBillProducts = _context.BillProducts
                .Select(bp => new
                {
                    bp.BillId,
                    bp.BillProductId,
                    ProductName = bp.Product.Title,
                    bp.Quantity,
                    Price = bp.ItemPrice,
                    Amount = bp.TotalPrice,
                    bp.IsReturn
                })
                .ToList();

            dgvBillProducts.DataSource = allBillProducts;
        }

        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBillID.Text, out int billId))
            {
                MessageBox.Show("Enter valid Bill ID");
                return;
            }

            var bill = _context.Bills.FirstOrDefault(b => b.BillId == billId);
            if (bill == null)
            {
                MessageBox.Show("Bill not found");
                return;
            }

            dgvBillProducts.DataSource = bill.BillProducts
                .Select(bp => new
                {
                    bp.BillId,
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

            var row = dgvBillProducts.Rows[e.RowIndex];

            int billId = Convert.ToInt32(row.Cells["BillId"].Value);
            int bpId = Convert.ToInt32(row.Cells["BillProductId"].Value);
            bool isReturned = row.Cells["IsReturn"].Value != null && Convert.ToBoolean(row.Cells["IsReturn"].Value);

            string user = SessionManager.UserName;

            if (dgvBillProducts.Columns[e.ColumnIndex].Name == "ReturnButtonColumn")
            {
                if (isReturned)
                {
                    MessageBox.Show("This product is already returned.");
                    return;
                }

                try
                {
                    _returnService.ReturnProduct(billId, bpId, user);
                    MessageBox.Show("Returned Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else if (dgvBillProducts.Columns[e.ColumnIndex].Name == "ModifyButtonColumn")
            {
                new ModifyReturnForm(billId, bpId, (ReturnService)_returnService, user).ShowDialog();
            }

            LoadAllSales(); // Refresh DataGridView
        }

        private void btnViewReturnHistoryForm_Click(object sender, EventArgs e)
        {
            new ViewReturnHistoryForm().Show();
        }
    }
}
