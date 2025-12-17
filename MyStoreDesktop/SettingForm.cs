using MyStoreDesktop.Theme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class SettingForm : Form
    {
        private SettingService _settingService = new SettingService();

        public SettingForm()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            SetupDataGridView();
            LoadSettings();
            LoadKeysToComboBox();
        }

        private void LoadSettings()
        {
            dgvSettings.DataSource = null;
            dgvSettings.DataSource = _settingService.GetAll();
        }

        private void LoadKeysToComboBox()
        {
            cmbKey.Items.Clear();
            foreach (var setting in _settingService.GetAll())
            {
                cmbKey.Items.Add(setting.Key);
            }
        }

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
                ReadOnly = true
            });

            dgvSettings.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Key",
                DataPropertyName = "Key",
                Name = "Key"
            });

            dgvSettings.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Value",
                DataPropertyName = "Value",
                Name = "Value"
            });

            var updateButton = new DataGridViewButtonColumn
            {
                Text = "Update",
                UseColumnTextForButtonValue = true,
                HeaderText = "Update"
            };
            dgvSettings.Columns.Add(updateButton);

            var deleteButton = new DataGridViewButtonColumn
            {
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                HeaderText = "Delete",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dgvSettings.Columns.Add(deleteButton);

            dgvSettings.CellClick += DgvSettings_CellClick;
        }

        private void DgvSettings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvSettings.Rows[e.RowIndex];
            string key = row.Cells["Key"].Value.ToString();
            string value = row.Cells["Value"].Value.ToString();

            if (dgvSettings.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                string buttonText = dgvSettings.Columns[e.ColumnIndex].HeaderText;

                if (buttonText == "Update")
                {
                    SettingUpdateViewPanel.Visible = true;
                }
                else if (buttonText == "Delete")
                {
                    var confirm = MessageBox.Show($"Are you sure you want to delete '{key}'?", "Confirm Delete", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        _settingService.Delete(key);
                        LoadSettings();
                        LoadKeysToComboBox();
                        MessageBox.Show("Deleted successfully!");
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string key = cmbKey.Text.Trim();
            string value = txtValue.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Key cannot be empty!");
                return;
            }

            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Value cannot be empty!");
                return;
            }
            _settingService.Add(key, value);
            LoadSettings();

        }

        private void SettingbtnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SettingUpdateViewPanel.Visible = false;
        }
    }
}
