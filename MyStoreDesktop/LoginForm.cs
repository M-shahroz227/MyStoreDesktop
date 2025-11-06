using System;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class LoginForm : Form
    {
        DatabaseHelper db = new DatabaseHelper();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role;

            DatabaseHelper db = new DatabaseHelper();

            if (db.ValidateUser(username, password, out role))
            {
                MessageBox.Show("Welcome " + username + "!", "Login Successful");

                // 🔹 Home form open karo
                Home home = new Home();
                home.Show();

                // 🔹 Login form hide kar do
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error");
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
