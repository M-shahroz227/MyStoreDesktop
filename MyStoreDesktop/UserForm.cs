using System;
using System.Drawing;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.UserService;
using MyStoreDesktop.Theme;

namespace MyStoreDesktop
{
    public partial class UserForm : Form
    {
        UserService _userService = new UserService();
        public UserForm()
        {
            InitializeComponent();

            // Apply professional blue theme
            ThemeManager.ApplyTheme(this);
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            // ComboBox options
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("User");

            // Setup Grid Columns
            dgvUsers.Columns.Clear();
            dgvUsers.Columns.Add("Username", "Username");
            dgvUsers.Columns.Add("Role", "Role");
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

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
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(txtPassword.Text, out passwordHash, out passwordSalt);

            var data = new User
            {
                UserName = txtUsername.Text,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt, 
                Role = cmbRole.Text,
            };
            _userService.Add(data);
            MessageBox.Show("data save Successfully");
            dgvUsers.Rows.Add(txtUsername.Text, cmbRole.Text);
            ClearFields();
        }

        public void CreatePasswordHash(string text, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash =hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                dgvUsers.CurrentRow.Cells["Username"].Value = txtUsername.Text;
                dgvUsers.CurrentRow.Cells["Role"].Value = cmbRole.Text;
                ClearFields();
            }
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(txtPassword.Text, out passwordHash, out passwordSalt);

            var data = new User
            {
                UserName = txtUsername.Text,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,


            };
            _userService.Update(data);
            MessageBox.Show("Data Update Successfully ");

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
