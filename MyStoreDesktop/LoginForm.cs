using System;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.UserService;

namespace MyStoreDesktop
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;

        public LoginForm()
        {
        }

        public LoginForm(Data.DatabaseHelper db)
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password", "Warning");
                return;
            }

            // 🔹 Get all users from DB
            var user = _userService.GetAll()
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                MessageBox.Show("User not found!", "Error");
                return;
            }

            // 🔹 Simple password validation (NOTE: add real hashing later)
            var dbPassword = System.Text.Encoding.UTF8.GetString(user.PasswordHash);
            if (dbPassword == password)
            {
                

                // 🔹 Open Home Form
                Home home = new Home();
                home.Show();

                // 🔹 Hide login
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid password!", "Error");
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            reg.Show();
            this.Hide();
        }
    }
}
