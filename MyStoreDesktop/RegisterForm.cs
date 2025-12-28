using MyStoreDesktop.Models;
using MyStoreDesktop.Services.UserService;
using MyStoreDesktop.Theme;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace MyStoreDesktop
{
    public partial class RegisterForm : Form
    {
        private readonly UserService _userService;

        public RegisterForm()
        {
            InitializeComponent();
            _userService = new UserService();

            // Apply professional blue theme
            ThemeManager.ApplyTheme(this);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string Email = txtFullEmail.Text.Trim();
            string Phone = txtFullPhone.Text.Trim();
            if (!ValidateRegister())
            {
                return;
            }

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
                LoginForm login = new LoginForm(null);
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
           var  login = new LoginForm(null);
            login.Show();
            this.Hide();
        }
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool IsStrongPassword(string password)
        {
            return password.Length >= 6 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }
        private bool ValidateRegister()
        {
            errorProvider1.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username required");
                valid = false;
            }

            if (!IsValidEmail(txtFullEmail.Text))
            {
                errorProvider1.SetError(txtFullEmail, "Invalid email format");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtFullPhone.Text) || txtFullPhone.Text.Length < 10)
            {
                errorProvider1.SetError(txtFullPhone, "Enter valid phone number");
                valid = false;
            }

            if (!IsStrongPassword(txtPassword.Text))
            {
                errorProvider1.SetError(
                    txtPassword,
                    "Password must contain Upper, Lower & Number (min 6 chars)"
                );
                valid = false;
            }

            return valid;
        }


    }
}