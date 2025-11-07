using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;

namespace MyStoreDesktop
{
    public class CompanyManagerForm : Form
    {
        private readonly IProductService _productService;
        private DataGridView dgvCompanies;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnClose;
        private int selectedCompanyId = 0;
        private bool changesMade = false;

        public int? SelectedCompanyId { get; private set; }

        public CompanyManagerForm(IProductService productService)
        {
            _productService = productService;
            InitializeComponent();
            LoadCompanies();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Companies";
            this.Size = new Size(620, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            dgvCompanies = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(560, 220),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvCompanies.CellClick += DgvCompanies_CellClick;

            Label lblTitle = new Label
            {
                Text = "Title",
                Location = new Point(20, 260),
                AutoSize = true
            };

            txtTitle = new TextBox
            {
                Location = new Point(120, 255),
                Size = new Size(460, 24)
            };

            Label lblDescription = new Label
            {
                Text = "Description",
                Location = new Point(20, 303),
                AutoSize = true
            };

            txtDescription = new TextBox
            {
                Location = new Point(120, 298),
                Size = new Size(460, 80),
                Multiline = true
            };

            btnAdd = new Button
            {
                Text = "Add",
                Location = new Point(20, 400),
                Size = new Size(90, 36)
            };
            btnAdd.Click += BtnAdd_Click;

            btnUpdate = new Button
            {
                Text = "Update",
                Location = new Point(120, 400),
                Size = new Size(90, 36)
            };
            btnUpdate.Click += BtnUpdate_Click;

            btnDelete = new Button
            {
                Text = "Delete",
                Location = new Point(220, 400),
                Size = new Size(90, 36)
            };
            btnDelete.Click += BtnDelete_Click;

            btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(320, 400),
                Size = new Size(90, 36)
            };
            btnClear.Click += BtnClear_Click;

            btnClose = new Button
            {
                Text = "Close",
                Location = new Point(490, 400),
                Size = new Size(90, 36)
            };
            btnClose.Click += BtnClose_Click;

            this.Controls.Add(dgvCompanies);
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitle);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClear);
            this.Controls.Add(btnClose);

            this.AcceptButton = btnAdd;
            this.CancelButton = btnClose;
        }

        private void LoadCompanies()
        {
            var companies = _productService.GetCompanies()
                .Select(c => new
                {
                    c.CompanyId,
                    c.Title,
                    c.Description
                })
                .ToList();

            dgvCompanies.DataSource = companies;

            if (dgvCompanies.Columns.Contains("CompanyId"))
            {
                dgvCompanies.Columns["CompanyId"].Visible = false;
            }

            ReselectCompany();
        }

        private void ReselectCompany()
        {
            if (selectedCompanyId == 0)
            {
                dgvCompanies.ClearSelection();
                return;
            }

            foreach (DataGridViewRow row in dgvCompanies.Rows)
            {
                if (row.Cells["CompanyId"].Value is int id && id == selectedCompanyId)
                {
                    row.Selected = true;
                    dgvCompanies.CurrentCell = row.Cells["Title"];
                    break;
                }
            }
        }

        private void DgvCompanies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvCompanies.Rows[e.RowIndex];
            selectedCompanyId = Convert.ToInt32(row.Cells["CompanyId"].Value);
            txtTitle.Text = row.Cells["Title"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
            SelectedCompanyId = selectedCompanyId;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var company = new Company
            {
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            _productService.AddCompany(company);
            changesMade = true;
            selectedCompanyId = company.CompanyId;
            SelectedCompanyId = selectedCompanyId;
            LoadCompanies();
            MessageBox.Show("Company added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCompanyId == 0)
            {
                MessageBox.Show("Please select a company to update.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var company = _productService.GetCompanyById(selectedCompanyId);
            if (company == null)
            {
                MessageBox.Show("Unable to load selected company.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            company.Title = txtTitle.Text.Trim();
            company.Description = txtDescription.Text.Trim();

            _productService.UpdateCompany(company);
            changesMade = true;
            SelectedCompanyId = selectedCompanyId;
            LoadCompanies();
            MessageBox.Show("Company updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCompanyId == 0)
            {
                MessageBox.Show("Please select a company to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this company?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _productService.DeleteCompany(selectedCompanyId);
                changesMade = true;
                SelectedCompanyId = null;
                selectedCompanyId = 0;
                ClearFormFields();
                LoadCompanies();
                MessageBox.Show("Company deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            dgvCompanies.ClearSelection();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = changesMade ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        private void ClearFormFields()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            selectedCompanyId = 0;
            SelectedCompanyId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = changesMade ? DialogResult.OK : DialogResult.Cancel;
            }

            base.OnFormClosing(e);
        }
    }
}

