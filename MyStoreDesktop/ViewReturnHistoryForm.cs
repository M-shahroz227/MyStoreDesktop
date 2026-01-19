using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using Newtonsoft.Json;

namespace MyStoreDesktop.Forms
{
    public partial class ViewReturnHistoryForm : Form
    {
        private readonly DatabaseHelper _context;

        private DataGridView dgvHistory;
        private Label lblTitle;
        private Button btnClose;

        public ViewReturnHistoryForm()
        {
            _context = new DatabaseHelper();

            // ---------------- Form Properties ----------------
            this.Text = "Return History";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ClientSize = new Size(800, 500);
            this.BackColor = Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeControls();
            LoadHistory();
           
        }

        private void InitializeControls()
        {
            // Title
            lblTitle = new Label()
            {
                Text = "Return History",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 20,
                Left = 20
            };
            this.Controls.Add(lblTitle);

            // DataGridView
            dgvHistory = new DataGridView()
            {
                Left = 20,
                Top = 60,
                Width = 750,
                Height = 380,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvHistory);

            // Close button
            btnClose = new Button()
            {
                Text = "Close",
                Left = 680,
                Top = 450,
                Width = 90,
                Height = 35,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void LoadHistory()
        {
            var historyData = _context.BillHistories
                .OrderByDescending(h => h.ModifiedOn)
                .Select(h => new
                {
                    h.BillHistoryId,
                    h.BillId,
                    h.ModifiedOn,
                    Before = h.BeforeJson,
                    After = h.AfterJson,
                    Json = h.SnapshotJson
                })
                .ToList();

            dgvHistory.DataSource = historyData;

            // Optional: Hide raw JSON if you want
            dgvHistory.Columns["Before"].Visible = false;
            dgvHistory.Columns["After"].Visible = false;
            dgvHistory.Columns["Json"].Visible = false;

            // Add a "View Details" button column
            var btnDetails = new DataGridViewButtonColumn()
            {
                HeaderText = "Action",
                Text = "View Details",
                Name = "ViewDetailsColumn",
                UseColumnTextForButtonValue = true
            };
            dgvHistory.Columns.Add(btnDetails);

            dgvHistory.CellContentClick += DgvHistory_CellContentClick;
        }

        private void DgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvHistory.Columns[e.ColumnIndex].Name == "ViewDetailsColumn")
            {
                // Get selected history
                int historyId = Convert.ToInt32(dgvHistory.Rows[e.RowIndex].Cells["BillHistoryId"].Value);
                var history = _context.BillHistories.FirstOrDefault(h => h.BillHistoryId == historyId);
                if (history != null)
                {
                    string beforeJson = history.BeforeJson;
                    string afterJson = history.AfterJson;

                    // Show Before & After in messagebox (or create another form if you want fancy UI)
                    MessageBox.Show(
                        "Before:\n" + FormatJson(beforeJson) + "\n\nAfter:\n" + FormatJson(afterJson),
                        $"Bill History Details - Bill {history.BillId}",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private string FormatJson(string json)
        {
            try
            {
                var parsed = Newtonsoft.Json.Linq.JToken.Parse(json);
                return parsed.ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch
            {
                return json;
            }
        }
    }
}
