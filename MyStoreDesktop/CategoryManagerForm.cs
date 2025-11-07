using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;

namespace MyStoreDesktop
{
    public class CategoryManagerForm : Form
    {
        private readonly IProductService _productService;
        private DataGridView dgvCategories;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnClose;
        private int selectedCategoryId = 0;
        private bool changesMade = false;

        public int? SelectedCategoryId { get; private set; }

        public CategoryManagerForm(IProductService productService)
        {
            _productService = productService;
            InitializeComponent();
            LoadCategories();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Categories";
            this.Size = new Size(620, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            dgvCategories = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(560, 220),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvCategories.CellClick += DgvCategories_CellClick;

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

            this.Controls.Add(dgvCategories);
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

        private void LoadCategories()
        {
            var categories = _productService.GetCategories()
                .Select(c => new
                {
                    c.CategoryId,
                    c.Title,
                    c.Description
                })
                .ToList();

            dgvCategories.DataSource = categories;

            if (dgvCategories.Columns.Contains("CategoryId"))
            {
                dgvCategories.Columns["CategoryId"].Visible = false;
            }

            ReselectCategory();
        }

        private void ReselectCategory()
        {
            if (selectedCategoryId == 0)
            {
                dgvCategories.ClearSelection();
                return;
            }

            foreach (DataGridViewRow row in dgvCategories.Rows)
            {
                if (row.Cells["CategoryId"].Value is int id && id == selectedCategoryId)
                {
                    row.Selected = true;
                    dgvCategories.CurrentCell = row.Cells["Title"];
                    break;
                }
            }
        }

        private void DgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgvCategories.Rows[e.RowIndex];
            selectedCategoryId = Convert.ToInt32(row.Cells["CategoryId"].Value);
            txtTitle.Text = row.Cells["Title"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
            SelectedCategoryId = selectedCategoryId;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = new Category
            {
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            _productService.AddCategory(category);
            changesMade = true;
            selectedCategoryId = category.CategoryId;
            SelectedCategoryId = selectedCategoryId;
            LoadCategories();
            MessageBox.Show("Category added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == 0)
            {
                MessageBox.Show("Please select a category to update.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = _productService.GetCategoryById(selectedCategoryId);
            if (category == null)
            {
                MessageBox.Show("Unable to load selected category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            category.Title = txtTitle.Text.Trim();
            category.Description = txtDescription.Text.Trim();

            _productService.UpdateCategory(category);
            changesMade = true;
            SelectedCategoryId = selectedCategoryId;
            LoadCategories();
            MessageBox.Show("Category updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == 0)
            {
                MessageBox.Show("Please select a category to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this category?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _productService.DeleteCategory(selectedCategoryId);
                changesMade = true;
                SelectedCategoryId = null;
                selectedCategoryId = 0;
                ClearFormFields();
                LoadCategories();
                MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            dgvCategories.ClearSelection();
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
            selectedCategoryId = 0;
            SelectedCategoryId = null;
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

