using MyStoreDesktop.Theme;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class SettingForm : Form
    {
        private readonly SettingService _settingService = new SettingService();
        private string _updateKey = string.Empty;

        public SettingForm()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            SetupDataGridView();
            LoadSettings();
            LoadKeysToComboBox();
        }

        // ================= LOAD =================

        private void LoadSettings()
        {
            dgvSettings.DataSource = null;
            dgvSettings.DataSource = _settingService.GetAll();
        }

        private void LoadKeysToComboBox()
        {
            cmbKey.Items.Clear();
            cmbKey.Items.Add("BasePath");
            cmbKey.Items.Add("StoreName");
        }

        // ================= GRID =================

        private void SetupDataGridView()
        {
            SettingUpdateViewPanel.Visible = false;

            dgvSettings.AutoGenerateColumns = false;
            dgvSettings.Columns.Clear();

            dgvSettings.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                Name = "Id",
                Width = 50,
                ReadOnly = true
            });

            dgvSettings.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Key",
                DataPropertyName = "Key",
                Name = "Key",
                Width = 150,
                ReadOnly = true
            });

            dgvSettings.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Value",
                DataPropertyName = "Value",
                Name = "Value",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvSettings.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Update",
                Text = "Update",
                UseColumnTextForButtonValue = true,
                Width = 80
            });

            dgvSettings.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 80
            });

            dgvSettings.CellClick += DgvSettings_CellClick;
        }

        // ================= GRID EVENTS =================

        private void DgvSettings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dgvSettings.Columns[e.ColumnIndex].HeaderText;
            string key = dgvSettings.Rows[e.RowIndex].Cells["Key"].Value.ToString();
            string value = dgvSettings.Rows[e.RowIndex].Cells["Value"].Value.ToString();

            if (columnName == "Update")
            {
                _updateKey = key;

                lblUpdateKey.Text = key;
                txtSUpdateValue.Text = value;

                SettingUpdateViewPanel.Visible = true;
                SettingUpdateViewPanel.BringToFront();
            }
            else if (columnName == "Delete")
            {
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete '{key}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    _settingService.Delete(key);
                    LoadSettings();
                    MessageBox.Show("Deleted successfully!");
                }
            }
        }

        // ================= ADD =================

        private void btnSave_Click(object sender, EventArgs e)
        {
            string key = cmbKey.Text.Trim();
            string value = txtValue.Text.Trim();

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Key and Value are required!");
                return;
            }

            var existing = _settingService.GetAll()
                .FirstOrDefault(x => x.Key == key);

            if (existing != null)
            {
                _settingService.Update(key, value);
                MessageBox.Show("Updated successfully!");
            }
            else
            {
                _settingService.Add(key, value);
                MessageBox.Show("Added successfully!");
            }

            txtValue.Clear();
            LoadSettings();
        }

        // ================= UPDATE PANEL =================

        private void SettingbtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_updateKey))
            {
                MessageBox.Show("No setting selected!");
                return;
            }

            string newValue = txtSUpdateValue.Text.Trim();

            if (string.IsNullOrEmpty(newValue))
            {
                MessageBox.Show("Value cannot be empty!");
                return;
            }

            _settingService.Update(_updateKey, newValue);

            SettingUpdateViewPanel.Visible = false;
            LoadSettings();

            MessageBox.Show("Setting updated successfully!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SettingUpdateViewPanel.Visible = false;
        }
    }
}
