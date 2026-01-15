using MyStoreDesktop.Models;
using MyStoreDesktop.Services;
using MyStoreDesktop.Theme;
using System;
using System.Linq;
using System.Windows.Forms;

namespace POSApp
{
    public partial class FrmReturnHistory : Form
    {
        private DataGridView dgvReturns;
        private Button btnRefresh, btnDelete;
        private Label lblStatus;

        private readonly IReturnService _returnService;

        public FrmReturnHistory()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            _returnService = new ReturnService();
            InitializeForm();
            LoadReturnHistory();
        }

        private void InitializeForm()
        {
            this.Text = "Return History";
            this.Width = 900;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvReturns = new DataGridView
            {
                Left = 20,
                Top = 20,
                Width = 840,
                Height = 380,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            btnRefresh = new Button { Text = "Refresh", Left = 20, Top = 410, Width = 120 };
            btnRefresh.Click += (s, e) => LoadReturnHistory();

            btnDelete = new Button { Text = "Delete Selected Return(s)", Left = 160, Top = 410, Width = 180 };
            btnDelete.Click += BtnDelete_Click;

            lblStatus = new Label { Left = 360, Top = 415, Width = 500, ForeColor = System.Drawing.Color.Blue };

            this.Controls.Add(dgvReturns);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnDelete);
            this.Controls.Add(lblStatus);
        }

        private void LoadReturnHistory()
        {
            try
            {
                var returnItems = _returnService.GetAllReturnItems()
                    .Select(r => new
                    {
                        r.ReturnItemId,
                        r.ReturnId,
                        BillId = r.Return.BillId,
                        r.Product.Title,
                        r.ReturnQuantity,
                        r.ItemPrice,
                        r.TotalPrice,
                        ReturnDate = r.Return.ReturnDate
                    }).ToList();

                dgvReturns.DataSource = returnItems;
                lblStatus.Text = $"{returnItems.Count} return item(s) loaded.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading return history: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReturns.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select return item(s) to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete selected return(s)?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                foreach (DataGridViewRow row in dgvReturns.SelectedRows)
                {
                    int returnItemId = Convert.ToInt32(row.Cells["ReturnItemId"].Value);
                    _returnService.DeleteReturnItem(returnItemId);
                }

                MessageBox.Show("Selected return item(s) deleted successfully.");
                LoadReturnHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting return item(s): {ex.Message}");
            }
        }
    }
}
