using MyStoreDesktop.Services;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSApp
{
    public partial class FrmGoogleDriveBackup : Form
    {
        TextBox txtEmail;
        Button btnBackup, btnRestore;
        CheckBox chkRemember;
        Label lblTitle, lblEmail, lblStatus;

        private readonly ISettingService _settingService;

        public FrmGoogleDriveBackup()
        {
            InitializeComponent();
            _settingService = new SettingService();
            CreateUI();
            this.Load += FrmGoogleDriveBackup_Load;
        }

        private void CreateUI()
        {
            this.Text = _settingService.GetAppName() + " - Google Drive Backup";
            this.Size = new Size(520, 330);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblTitle = new Label
            {
                Text = _settingService.GetStoreName() + " Google Drive Backup",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(130, 20)
            };
            Controls.Add(lblTitle);

            lblEmail = new Label
            {
                Text = "Google Gmail:",
                Location = new Point(50, 80),
                AutoSize = true
            };
            Controls.Add(lblEmail);

            txtEmail = new TextBox
            {
                Location = new Point(150, 78),
                Width = 300
            };
            Controls.Add(txtEmail);

            chkRemember = new CheckBox
            {
                Text = "Remember Gmail",
                Location = new Point(150, 110),
                AutoSize = true
            };
            Controls.Add(chkRemember);

            btnBackup = new Button
            {
                Text = "Backup to Google Drive",
                Size = new Size(190, 36),
                Location = new Point(50, 150),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White
            };
            btnBackup.Click += async (s, e) => await BtnBackup_Click();
            Controls.Add(btnBackup);

            btnRestore = new Button
            {
                Text = "Restore from Google Drive",
                Size = new Size(190, 36),
                Location = new Point(260, 150),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White
            };
            btnRestore.Click += async (s, e) => await BtnRestore_Click();
            Controls.Add(btnRestore);

            lblStatus = new Label
            {
                Text = "Status: Waiting...",
                Location = new Point(50, 220),
                AutoSize = true
            };
            Controls.Add(lblStatus);
        }

        private void FrmGoogleDriveBackup_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MyStoreDesktop.Properties.Settings.Default.LastGmail))
            {
                txtEmail.Text = MyStoreDesktop.Properties.Settings.Default.LastGmail;
                chkRemember.Checked = true;
            }
        }

        private bool IsValidGmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email &&
                       email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private async Task BtnBackup_Click()
        {
            try
            {
                string gmail = txtEmail.Text.Trim();

                if (!IsValidGmail(gmail))
                {
                    MessageBox.Show("Please enter a valid Gmail address");
                    return;
                }

                lblStatus.Text = "Authorizing Google Drive...";
                Application.DoEvents();

                var service = await GoogleDriveServiceHelper.GetServiceAsync(gmail, _settingService);

                lblStatus.Text = "Creating database backup...";
                Application.DoEvents();

                string backupDir = Path.Combine(_settingService.GetBasePath(), "Backup");
                Directory.CreateDirectory(backupDir);

                string backupFile = Path.Combine(backupDir, $"{_settingService.GetStoreName()}_{DateTime.Now:yyyyMMdd_HHmmss}.bak");

                string sqlConn = @"Server=DESKTOP-6UR01QA\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
                string backupSql = $"BACKUP DATABASE MyStore TO DISK='{backupFile}' WITH INIT";

                using (SqlConnection con = new SqlConnection(sqlConn))
                {
                    con.Open();
                    new SqlCommand(backupSql, con).ExecuteNonQuery();
                }

                lblStatus.Text = "Uploading to Google Drive...";
                Application.DoEvents();

                string folderId = await GoogleDriveServiceHelper.GetOrCreateFolderAsync(service, _settingService.GetAppName());

                var fileMeta = new Google.Apis.Drive.v3.Data.File
                {
                    Name = Path.GetFileName(backupFile),
                    Parents = new System.Collections.Generic.List<string> { folderId }
                };

                using (var fs = new FileStream(backupFile, FileMode.Open, FileAccess.Read))
                {
                    var request = service.Files.Create(fileMeta, fs, "application/octet-stream");
                    await request.UploadAsync();
                }

                if (chkRemember.Checked)
                    MyStoreDesktop.Properties.Settings.Default.LastGmail = gmail;
                else
                    MyStoreDesktop.Properties.Settings.Default.LastGmail = "";

                MyStoreDesktop.Properties.Settings.Default.Save();

                lblStatus.Text = "Backup uploaded successfully ✅";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Backup Error");
                lblStatus.Text = "Backup failed ❌";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private async Task BtnRestore_Click()
        {
            try
            {
                string gmail = txtEmail.Text.Trim();

                if (!IsValidGmail(gmail))
                {
                    MessageBox.Show("Please enter a valid Gmail address");
                    return;
                }

                lblStatus.Text = "Connecting to Google Drive...";
                Application.DoEvents();

                var service = await GoogleDriveServiceHelper.GetServiceAsync(gmail, _settingService);

                string folderId = await GoogleDriveServiceHelper.GetOrCreateFolderAsync(service, _settingService.GetAppName());

                var listReq = service.Files.List();
                listReq.Q = $"'{folderId}' in parents and name contains '{_settingService.GetStoreName()}'";
                listReq.Fields = "files(id,name,createdTime)";

                var files = await listReq.ExecuteAsync();

                if (files.Files == null || files.Files.Count == 0)
                {
                    MessageBox.Show("No backup found on Google Drive");
                    return;
                }

                var latest = files.Files.OrderByDescending(f => f.CreatedTime).First();

                string restoreDir = Path.Combine(_settingService.GetBasePath(), "Restore");
                Directory.CreateDirectory(restoreDir);

                string restorePath = Path.Combine(restoreDir, latest.Name);

                lblStatus.Text = "Downloading backup...";
                Application.DoEvents();

                using (var fs = new FileStream(restorePath, FileMode.Create))
                {
                    await service.Files.Get(latest.Id).DownloadAsync(fs);
                }

                lblStatus.Text = "Restore file downloaded successfully ✅";
                lblStatus.ForeColor = Color.Green;

                MessageBox.Show(
                    "Backup file downloaded.\n\nRestore it manually in SQL Server.",
                    "Restore Complete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Restore Error");
                lblStatus.Text = "Restore failed ❌";
                lblStatus.ForeColor = Color.Red;
            }
        }
    }
}
