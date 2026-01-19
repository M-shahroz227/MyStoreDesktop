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

            this.Load += ManagementBillForm_Load;   // ✅ Form Load Event
        }

        private void ManagementBillForm_Load(object sender, EventArgs e)
        {
            LoadAllSales();   // auto load BillProducts
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
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Product", DataPropertyName = "ProductName" });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = "Quantity" });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = "Price" });
            dgvBillProducts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount", DataPropertyName = "Amount" });
            dgvBillProducts.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Returned", DataPropertyName = "IsReturn" });

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
                HeaderText = "Action",
                Text = "Modify",
                UseColumnTextForButtonValue = true
            });

            dgvBillProducts.CellContentClick += DgvBillProducts_CellContentClick;
        }

        private void LoadAllSales()
        {
            dgvBillProducts.DataSource = _context.BillProducts
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
        }

        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBillID.Text, out int billId))
            {
                MessageBox.Show("Enter valid Bill ID"); return;
            }

            var bill = _context.Bills.FirstOrDefault(b => b.BillId == billId);
            if (bill == null)
            {
                MessageBox.Show("Bill not found"); return;
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

            if (!int.TryParse(txtBillID.Text, out int billId))
            {
                MessageBox.Show("Enter Bill ID first"); return;
            }

            string user = SessionManager.UserName;
            int bpId = Convert.ToInt32(dgvBillProducts.Rows[e.RowIndex].Cells["BillProductId"].Value);

            if (dgvBillProducts.Columns[e.ColumnIndex].Name == "ReturnButtonColumn")
            {
                _returnService.ReturnProduct(billId, bpId, user);
                MessageBox.Show("Returned Successfully");
            }
            else if (dgvBillProducts.Columns[e.ColumnIndex].Name == "ModifyButtonColumn")
            {
                new ModifyReturnForm(billId, bpId, (ReturnService)_returnService, user).ShowDialog();
            }

            LoadAllSales(); // refresh grid
        }

        private void btnViewReturnHistoryForm_Click(object sender, EventArgs e)
        {
            new ViewReturnHistoryForm().Show();
        }
    }
}
