using System;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class RegisterForm : Form
    {
        DatabaseHelper db = new DatabaseHelper();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string fullname = txtFullName.Text;

            DatabaseHelper db = new DatabaseHelper();

            if (db.RegisterUser(username, password, fullname))
            {
                MessageBox.Show("Registration successful! Please login.");
                this.Close(); // Register form band kar do
            }
            else
            {
                MessageBox.Show("Error: Username may already exist.");
            }
        }


        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }
    }
}
