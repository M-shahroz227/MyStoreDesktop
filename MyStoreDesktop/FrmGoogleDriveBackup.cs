using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSApp
{
    public partial class FrmGoogleDriveBackup : Form
    {
        TextBox txtEmail;
        Button btnBackup;
        Button btnRestore;
        Label lblTitle;
        Label lblEmail;
        Label lblStatus;

        public FrmGoogleDriveBackup()
        {
            CreateUI(); // ✅ sirf ye call hoga
        }

        private void CreateUI()
        {
            // ===== Form Settings =====
            this.Text = "Google Drive Backup";
            this.Size = new Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // ===== Title =====
            lblTitle = new Label
            {
                Text = "POS Google Drive Backup",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(120, 20)
            };
            this.Controls.Add(lblTitle);

            // ===== Email Label =====
            lblEmail = new Label
            {
                Text = "Google Email:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(50, 80)
            };
            this.Controls.Add(lblEmail);

            // ===== Email TextBox =====
            txtEmail = new TextBox
            {
                Name = "txtEmail",
                Size = new Size(300, 25),
                Location = new Point(150, 78)
            };
            this.Controls.Add(txtEmail);

            // ===== Backup Button =====
            btnBackup = new Button
            {
                Name = "btnBackup",
                Text = "Backup to Google Drive",
                Size = new Size(180, 35),
                Location = new Point(50, 140),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBackup.Click += BtnBackup_Click;
            this.Controls.Add(btnBackup);

            // ===== Restore Button =====
            btnRestore = new Button
            {
                Name = "btnRestore",
                Text = "Restore from Google Drive",
                Size = new Size(180, 35),
                Location = new Point(250, 140),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRestore.Click += BtnRestore_Click;
            this.Controls.Add(btnRestore);

            // ===== Status Label =====
            lblStatus = new Label
            {
                Text = "Status: Waiting...",
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                ForeColor = Color.DarkSlateGray,
                Location = new Point(50, 200)
            };
            this.Controls.Add(lblStatus);
        }

        // ================= EVENTS =================

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Google Email",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            lblStatus.Text = "Status: Backing up data...";
            lblStatus.ForeColor = Color.DarkOrange;

            // 🔹 Future: Google Drive Backup call yahan hogi
            lblStatus.Text = "Status: Backup completed successfully!";
            lblStatus.ForeColor = Color.Green;
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Google Email",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            lblStatus.Text = "Status: Restoring data...";
            lblStatus.ForeColor = Color.DarkOrange;

            // 🔹 Future: Google Drive Restore call yahan hogi
            lblStatus.Text = "Status: Restore completed successfully!";
            lblStatus.ForeColor = Color.Green;
        }
    }
}
