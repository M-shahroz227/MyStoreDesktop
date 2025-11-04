using System;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            // ComboBox options
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Cashier");

            // Setup Grid Columns
            dgvUsers.Columns.Clear();
            dgvUsers.Columns.Add("Username", "Username");
            dgvUsers.Columns.Add("Role", "Role");

            var editBtn = new DataGridViewButtonColumn()
            {
                Name = "Edit",
                Text = "✎",
                UseColumnTextForButtonValue = true
            };
            var delBtn = new DataGridViewButtonColumn()
            {
                Name = "Delete",
                Text = "🗑",
                UseColumnTextForButtonValue = true
            };

            dgvUsers.Columns.Add(editBtn);
            dgvUsers.Columns.Add(delBtn);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(cmbRole.Text))
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }

            dgvUsers.Rows.Add(txtUsername.Text, cmbRole.Text);
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                dgvUsers.CurrentRow.Cells["Username"].Value = txtUsername.Text;
                dgvUsers.CurrentRow.Cells["Role"].Value = cmbRole.Text;
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
                dgvUsers.Rows.Remove(dgvUsers.CurrentRow);
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
        }
    }
}
