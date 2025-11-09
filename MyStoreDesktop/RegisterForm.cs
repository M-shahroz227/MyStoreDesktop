using System;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.UserService;

namespace MyStoreDesktop
{
    public partial class RegisterForm : Form
    {
        private readonly UserService _userService;

        public RegisterForm()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string Email = txtFullEmail.Text.Trim();
            string Phone = txtFullPhone.Text.Trim();

            // simple password hash example
            byte[] passwordHash = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] passwordSalt = new byte[0]; // aap later salting add kar sakte ho

            var user = new User
            {
                UserName = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = Email,
                Phone = Phone

            };

            try
            {
                _userService.Add(user);
                MessageBox.Show("Registration successful! Please login.");
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home login = new Home();
            login.Show();
            this.Hide();
        }
    }
}
