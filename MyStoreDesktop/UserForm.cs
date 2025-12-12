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
            if(selectedIndex == -1)
            {
                MessageBox.Show("Please select a row first");
                return;
            }
            
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
            dgvUsers.Rows.Add(data.Id, data.UserName, data.Role);
            ClearFields();


        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIndex == -1) 
            {
                int id = Convert.ToInt32(dgvUsers.Rows[selectedIndex].Cells["Id"].Value);
                _userService.Delete(id);
                dgvUsers.Rows.RemoveAt(selectedIndex);
                MessageBox.Show("User Delete Successfully");
                selectedIndex = -1;
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please Select User First");
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
            if(dgvUsers.CurrentRow != null)
            {
                selectedIndex = e.RowIndex;
                txtUsername.Text = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                cmbRole.Text = dgvUsers.Rows[e.RowIndex].Cells["Role"].Value.ToString();
            }
        }
    }
}
