namespace MyStoreDesktop
{
    partial class ProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblSalePrice = new System.Windows.Forms.Label();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.btnManageCompanies = new System.Windows.Forms.Button();
            this.lblModel = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.btnManageCategories = new System.Windows.Forms.Button();
            this.panelQRCode = new System.Windows.Forms.Panel();
            this.btnGenerateQR = new System.Windows.Forms.Button();
            this.panelManual = new System.Windows.Forms.Panel();
            this.txtManualCode = new System.Windows.Forms.TextBox();
            this.btnSaveManualCode = new System.Windows.Forms.Button();
            this.panelBarcode = new System.Windows.Forms.Panel();
            this.btnGenerateBarcode = new System.Windows.Forms.Button();
            this.cmbCodeType = new System.Windows.Forms.ComboBox();
            this.picQRPreview = new System.Windows.Forms.PictureBox();
            this.picBarcodePreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panelQRCode.SuspendLayout();
            this.panelManual.SuspendLayout();
            this.panelBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcodePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(70, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(150, 35);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(220, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(150, 75);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(149, 21);
            this.cboCategory.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(30, 80);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(30, 120);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(150, 115);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(220, 20);
            this.txtQuantity.TabIndex = 5;
            // 
            // lblSalePrice
            // 
            this.lblSalePrice.AutoSize = true;
            this.lblSalePrice.Location = new System.Drawing.Point(30, 160);
            this.lblSalePrice.Name = "lblSalePrice";
            this.lblSalePrice.Size = new System.Drawing.Size(58, 13);
            this.lblSalePrice.TabIndex = 6;
            this.lblSalePrice.Text = "Sale Price:";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(150, 155);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(220, 20);
            this.txtSalePrice.TabIndex = 7;
            // 
            // lblPurchasePrice
            // 
            this.lblPurchasePrice.AutoSize = true;
            this.lblPurchasePrice.Location = new System.Drawing.Point(30, 200);
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new System.Drawing.Size(82, 13);
            this.lblPurchasePrice.TabIndex = 8;
            this.lblPurchasePrice.Text = "Purchase Price:";
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.Location = new System.Drawing.Point(150, 195);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(220, 20);
            this.txtPurchasePrice.TabIndex = 9;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(30, 240);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(52, 13);
            this.lblDiscount.TabIndex = 10;
            this.lblDiscount.Text = "Discount:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(150, 235);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(220, 20);
            this.txtDiscount.TabIndex = 11;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(30, 280);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(54, 13);
            this.lblCompany.TabIndex = 12;
            this.lblCompany.Text = "Company:";
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(150, 275);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(151, 21);
            this.cboCompany.TabIndex = 13;
            // 
            // btnManageCompanies
            // 
            this.btnManageCompanies.Location = new System.Drawing.Point(306, 274);
            this.btnManageCompanies.Name = "btnManageCompanies";
            this.btnManageCompanies.Size = new System.Drawing.Size(63, 21);
            this.btnManageCompanies.TabIndex = 14;
            this.btnManageCompanies.Text = "Manage";
            this.btnManageCompanies.UseVisualStyleBackColor = true;
            this.btnManageCompanies.Click += new System.EventHandler(this.btnManageCompanies_Click);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(30, 320);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(39, 13);
            this.lblModel.TabIndex = 14;
            this.lblModel.Text = "Model:";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(150, 315);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(220, 20);
            this.txtModel.TabIndex = 15;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(50, 517);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(159, 517);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 35);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Crimson;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(269, 517);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 35);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(400, 35);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(718, 370);
            this.dgvProducts.TabIndex = 19;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(150, 353);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(220, 20);
            this.txtDescription.TabIndex = 21;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(30, 358);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(60, 13);
            this.lbDescription.TabIndex = 20;
            this.lbDescription.Text = "Description";
            // 
            // btnManageCategories
            // 
            this.btnManageCategories.BackColor = System.Drawing.Color.GhostWhite;
            this.btnManageCategories.ForeColor = System.Drawing.Color.Black;
            this.btnManageCategories.Location = new System.Drawing.Point(304, 75);
            this.btnManageCategories.Name = "btnManageCategories";
            this.btnManageCategories.Size = new System.Drawing.Size(65, 23);
            this.btnManageCategories.TabIndex = 25;
            this.btnManageCategories.Text = "Manage Categories";
            this.btnManageCategories.UseVisualStyleBackColor = false;
            this.btnManageCategories.Click += new System.EventHandler(this.btnManageCategories_Click);
            // 
            // panelQRCode
            // 
            this.panelQRCode.Controls.Add(this.picQRPreview);
            this.panelQRCode.Controls.Add(this.btnGenerateQR);
            this.panelQRCode.Location = new System.Drawing.Point(117, 407);
            this.panelQRCode.Name = "panelQRCode";
            this.panelQRCode.Size = new System.Drawing.Size(242, 104);
            this.panelQRCode.TabIndex = 26;
            // 
            // btnGenerateQR
            // 
            this.btnGenerateQR.BackColor = System.Drawing.Color.SlateBlue;
            this.btnGenerateQR.ForeColor = System.Drawing.Color.White;
            this.btnGenerateQR.Location = new System.Drawing.Point(8, 34);
            this.btnGenerateQR.Name = "btnGenerateQR";
            this.btnGenerateQR.Size = new System.Drawing.Size(120, 35);
            this.btnGenerateQR.TabIndex = 25;
            this.btnGenerateQR.Text = "Generate QR Code";
            this.btnGenerateQR.UseVisualStyleBackColor = false;
            this.btnGenerateQR.Click += new System.EventHandler(this.btnGenerateQR_Click);
            // 
            // panelManual
            // 
            this.panelManual.Controls.Add(this.txtManualCode);
            this.panelManual.Controls.Add(this.btnSaveManualCode);
            this.panelManual.Location = new System.Drawing.Point(0, 10);
            this.panelManual.Name = "panelManual";
            this.panelManual.Size = new System.Drawing.Size(244, 100);
            this.panelManual.TabIndex = 27;
            // 
            // txtManualCode
            // 
            this.txtManualCode.Location = new System.Drawing.Point(15, 10);
            this.txtManualCode.Multiline = true;
            this.txtManualCode.Name = "txtManualCode";
            this.txtManualCode.Size = new System.Drawing.Size(195, 33);
            this.txtManualCode.TabIndex = 26;
            // 
            // btnSaveManualCode
            // 
            this.btnSaveManualCode.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSaveManualCode.ForeColor = System.Drawing.Color.White;
            this.btnSaveManualCode.Location = new System.Drawing.Point(52, 54);
            this.btnSaveManualCode.Name = "btnSaveManualCode";
            this.btnSaveManualCode.Size = new System.Drawing.Size(120, 35);
            this.btnSaveManualCode.TabIndex = 25;
            this.btnSaveManualCode.Text = "Generate QRManual";
            this.btnSaveManualCode.UseVisualStyleBackColor = false;
            this.btnSaveManualCode.Click += new System.EventHandler(this.btnSaveManualCode_Click);
            // 
            // panelBarcode
            // 
            this.panelBarcode.Controls.Add(this.picBarcodePreview);
            this.panelBarcode.Controls.Add(this.btnGenerateBarcode);
            this.panelBarcode.Controls.Add(this.panelManual);
            this.panelBarcode.Location = new System.Drawing.Point(117, 410);
            this.panelBarcode.Name = "panelBarcode";
            this.panelBarcode.Size = new System.Drawing.Size(250, 100);
            this.panelBarcode.TabIndex = 28;
            // 
            // btnGenerateBarcode
            // 
            this.btnGenerateBarcode.BackColor = System.Drawing.Color.SlateBlue;
            this.btnGenerateBarcode.ForeColor = System.Drawing.Color.White;
            this.btnGenerateBarcode.Location = new System.Drawing.Point(8, 34);
            this.btnGenerateBarcode.Name = "btnGenerateBarcode";
            this.btnGenerateBarcode.Size = new System.Drawing.Size(120, 35);
            this.btnGenerateBarcode.TabIndex = 25;
            this.btnGenerateBarcode.Text = "Generate BrCode";
            this.btnGenerateBarcode.UseVisualStyleBackColor = false;
            this.btnGenerateBarcode.Click += new System.EventHandler(this.btnGenerateBarcode_Click);
            // 
            // cmbCodeType
            // 
            this.cmbCodeType.FormattingEnabled = true;
            this.cmbCodeType.Items.AddRange(new object[] {
            "QR Code",
            "Bar Code",
            "Manual Code"});
            this.cmbCodeType.Location = new System.Drawing.Point(150, 380);
            this.cmbCodeType.Name = "cmbCodeType";
            this.cmbCodeType.Size = new System.Drawing.Size(219, 21);
            this.cmbCodeType.TabIndex = 29;
            // 
            // picQRPreview
            // 
            this.picQRPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQRPreview.Location = new System.Drawing.Point(148, 10);
            this.picQRPreview.Name = "picQRPreview";
            this.picQRPreview.Size = new System.Drawing.Size(80, 80);
            this.picQRPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQRPreview.TabIndex = 26;
            this.picQRPreview.TabStop = false;
            // 
            // picBarcodePreview
            // 
            this.picBarcodePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcodePreview.Location = new System.Drawing.Point(148, 10);
            this.picBarcodePreview.Name = "picBarcodePreview";
            this.picBarcodePreview.Size = new System.Drawing.Size(80, 80);
            this.picBarcodePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBarcodePreview.TabIndex = 26;
            this.picBarcodePreview.TabStop = false;
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1140, 561);
            this.Controls.Add(this.cmbCodeType);
            this.Controls.Add(this.panelBarcode);
            this.Controls.Add(this.panelQRCode);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.btnManageCompanies);
            this.Controls.Add(this.cboCompany);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.txtPurchasePrice);
            this.Controls.Add(this.lblPurchasePrice);
            this.Controls.Add(this.btnManageCategories);
            this.Controls.Add(this.txtSalePrice);
            this.Controls.Add(this.lblSalePrice);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Management";
            this.Load += new System.EventHandler(this.ProductForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panelQRCode.ResumeLayout(false);
            this.panelManual.ResumeLayout(false);
            this.panelManual.PerformLayout();
            this.panelBarcode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQRPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcodePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblSalePrice;
        private System.Windows.Forms.TextBox txtSalePrice;
        private System.Windows.Forms.Label lblPurchasePrice;
        private System.Windows.Forms.TextBox txtPurchasePrice;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.Button btnManageCompanies;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Button btnManageCategories;
        private System.Windows.Forms.Panel panelQRCode;
        private System.Windows.Forms.PictureBox picQRPreview;
        private System.Windows.Forms.Button btnGenerateQR;
        private System.Windows.Forms.Panel panelManual;
        private System.Windows.Forms.Button btnSaveManualCode;
        private System.Windows.Forms.PictureBox picBarcodePreview;
        private System.Windows.Forms.Panel panelBarcode;
        private System.Windows.Forms.Button btnGenerateBarcode;
        private System.Windows.Forms.ComboBox cmbCodeType;
        private System.Windows.Forms.TextBox txtManualCode;
    }
}