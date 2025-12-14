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
         int selectedIndex = -1;

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
            UserUpdateViewPanel.Visible = false;
            dgvUsers.Columns.Clear();
            dgvUsers.Columns.Add("Id", "Id");
            dgvUsers.Columns["Id"].Visible = false;
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
            LoadUsersToGrid();
        }
        private void LoadUsersToGrid()
        {
            dgvUsers.Rows.Clear(); // Purani rows clear kar do

            var users = _userService.GetAll(); // DB ya service se fetch karo

            foreach (var user in users)
            {
                dgvUsers.Rows.Add(user.Id, user.UserName, user.Role);
            }
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
            dgvUsers.Rows.Add(data.Id,data.UserName,data.Role);
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
            if (selectedIndex < 0 || selectedIndex >= dgvUsers.Rows.Count)
            {
                MessageBox.Show("Please select a valid user first!");
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.Rows[selectedIndex].Cells["Id"].Value);

            byte[] passwordHash = null;
            byte[] passwordSalt = null;

            if (!string.IsNullOrWhiteSpace(UPassword.Text))
            {
                CreatePasswordHash(UPassword.Text, out passwordHash, out passwordSalt);
            }

            var user = new User
            {
                Id = userId,
                UserName = Username.Text,
                Role = URole.Text,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userService.Update(user);

            LoadUsersToGrid();  // This is enough, grid is refreshed

            MessageBox.Show("User Updated Successfully");

            UserUpdateViewPanel.Hide();
            dgvUsers.Visible = true;
            selectedIndex = -1;
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0 && selectedIndex < dgvUsers.Rows.Count)
            {
                int id = Convert.ToInt32(dgvUsers.Rows[selectedIndex].Cells["Id"].Value);
                _userService.Delete(id);
                dgvUsers.Rows.RemoveAt(selectedIndex);
                MessageBox.Show("User Deleted Successfully");
                selectedIndex = -1;
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a valid user first!");
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // ignore header clicks

            if (dgvUsers.Columns[e.ColumnIndex].Name == "Edit")
            {
                selectedIndex = e.RowIndex; // update selectedIndex here

                Username.Text = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                URole.Text = dgvUsers.Rows[e.RowIndex].Cells["Role"].Value.ToString();
                UPassword.Text = "";

                UserUpdateViewPanel.Visible = true;
                dgvUsers.Visible = false;
            }
            else if (dgvUsers.Columns[e.ColumnIndex].Name == "Delete")
            {
                selectedIndex = e.RowIndex; // update selectedIndex here
                int id = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells["Id"].Value);
                _userService.Delete(id);
                dgvUsers.Rows.RemoveAt(e.RowIndex);
                MessageBox.Show("User Deleted Successfully");
                selectedIndex = -1;
                ClearFields();
            }
        }
        private void UUVBtnClose_Click(object sender, EventArgs e)
        {
            UserUpdateViewPanel.Hide();
            dgvUsers.Visible = true;
            selectedIndex = -1;
            ClearFields();
        }


    }
}
