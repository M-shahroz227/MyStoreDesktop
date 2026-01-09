using System;
using System.Drawing;
using System.IO;
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
                MessageBox.Show("Please enter Google Email");
                return;
            }

            try
            {
                lblStatus.Text = "Status: Uploading backup to Google Drive...";
                lblStatus.ForeColor = Color.DarkOrange;
                Application.DoEvents();

                var service = GoogleDriveServiceHelper.GetService();

                string backupFilePath = @"C:\POS\Backup\POS_Backup.db"; // 👈 apni file
                string driveFileName = "POS_Backup.db";

                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = driveFileName
                };

                using (var stream = new FileStream(backupFilePath, FileMode.Open))
                {
                    var request = service.Files.Create(
                        fileMetadata,
                        stream,
                        "application/octet-stream"
                    );
                    request.Upload();
                }

                lblStatus.Text = "Status: Backup uploaded successfully ✅";
                lblStatus.ForeColor = Color.Green;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Backup Error");
                lblStatus.Text = "Status: Backup failed ❌";
                lblStatus.ForeColor = Color.Red;
            }
        }


        private void BtnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Status: Restoring from Google Drive...";
                lblStatus.ForeColor = Color.DarkOrange;
                Application.DoEvents();

                var service = GoogleDriveServiceHelper.GetService();

                // 1️⃣ File search
                var listRequest = service.Files.List();
                listRequest.Q = "name='POS_Backup.db'";
                listRequest.Fields = "files(id, name)";
                var files = listRequest.Execute().Files;

                if (files.Count == 0)
                {
                    MessageBox.Show("Backup file not found on Drive");
                    return;
                }

                // 2️⃣ Download
                var request = service.Files.Get(files[0].Id);
                using (var stream = new FileStream(@"C:\POS\Restore\POS_Backup.db", FileMode.Create))
                {
                    request.Download(stream);
                }

                lblStatus.Text = "Status: Restore completed successfully ✅";
                lblStatus.ForeColor = Color.Green;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Restore Error");
                lblStatus.Text = "Status: Restore failed ❌";
                lblStatus.ForeColor = Color.Red;
            }
        }

    }
}
