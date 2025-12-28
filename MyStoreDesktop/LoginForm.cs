using MyStoreDesktop.Models;
using MyStoreDesktop.Services;
using MyStoreDesktop.Services.UserService;
using MyStoreDesktop.Theme;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;
        private PictureBox picLoader;
        private Timer rotateTimer;
        private float angle = 0;

        public LoginForm(Data.DatabaseHelper db)
        {
            InitializeComponent();

            _userService = new UserService();

            // 🔹 Initialize Loader PictureBox
            InitializeLoader();

            // 🔹 Apply theme
            ThemeManager.ApplyTheme(this);

            // 🔹 Load saved credentials
            LoadSavedCredentials();
        }

        // ==================== LOADER ====================
        private void InitializeLoader()
        {
            picLoader = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(150, 150),
                Location = new Point(
                    (this.ClientSize.Width - 150) / 2,
                    (this.ClientSize.Height - 150) / 2
                ),
                BackColor = Color.Transparent,
                Visible = false
            };

            string loaderPath = @"E:\image\LOADER2.png";

            if (System.IO.File.Exists(loaderPath))
            {
                picLoader.Image = Image.FromFile(loaderPath);
            }
            else
            {
                MessageBox.Show("Loader image NOT FOUND:\n" + loaderPath);
            }

            this.Controls.Add(picLoader);
            picLoader.BringToFront();

            // 🔹 Initialize rotation timer
            rotateTimer = new Timer();
            rotateTimer.Interval = 50; 
            rotateTimer.Tick += RotateTimer_Tick;
        }

        private void RotateTimer_Tick(object sender, EventArgs e)
        {
            if (picLoader.Image == null) return;

            angle += 10; // rotate 10 degrees each tick
            if (angle >= 360) angle = 0;

            picLoader.Image = RotateImage((Bitmap)picLoader.Image, angle);
        }

        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            Bitmap rotated = new Bitmap(bmp.Width, bmp.Height);
            rotated.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
            using (Graphics g = Graphics.FromImage(rotated))
            {
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                g.DrawImage(bmp, new Point(0, 0));
            }
            return rotated;
        }

        // ==================== SHOW/HIDE LOADER ====================
        private void ShowLoader()
        {
            // Hide all controls except loader
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != picLoader)
                    ctrl.Visible = false;
            }

            picLoader.Visible = true;
            picLoader.BringToFront();
            rotateTimer.Start();
            Application.DoEvents();
        }

        private void HideLoader()
        {
            rotateTimer.Stop();
            foreach (Control ctrl in this.Controls)
                ctrl.Visible = true;

            picLoader.Visible = false;
        }

        // ==================== LOGIN ====================
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateLogin())
            {
                HideLoader();
                return;
            }
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            

            ShowLoader();
            await Task.Delay(500); // optional delay

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password", "Warning");
                HideLoader();
                return;
            }

            var user = _userService.GetAll()
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                MessageBox.Show("User not found!", "Error");
                HideLoader();
                return;
            }

            var dbPassword = System.Text.Encoding.UTF8.GetString(user.PasswordHash);
            if (dbPassword == password)
            {
                SessionManager.UserId = user.Id;
                SessionManager.UserName = user.UserName;
                SessionManager.Role = user.Role;
                if (chkRememberMe.Checked)
                    CredentialManager.SaveCredentials(username, password);
                else
                    CredentialManager.ClearCredentials();

                Home home = new Home();
                home.FormClosed += (s, args) => Application.Exit();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid password!", "Error");
            }

            HideLoader();
        }

        // ==================== REMEMBER ME ====================
        private void LoadSavedCredentials()
        {
            var (username, password, rememberMe) = CredentialManager.LoadCredentials();

            if (rememberMe)
            {
                txtUsername.Text = username;
                txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            reg.Show();
            this.Hide();
        }
        private bool ValidateLogin()
        {
            errorProvider1.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username is required");
                isValid = false;
            }
            else if (txtUsername.Text.Length < 3)
            {
                errorProvider1.SetError(txtUsername, "Minimum 3 characters");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password is required");
                isValid = false;
            }
            else if (txtPassword.Text.Length < 6)
            {
                errorProvider1.SetError(txtPassword, "Password must be at least 6 characters");
                isValid = false;
            }

            return isValid;
        }



    }
}
