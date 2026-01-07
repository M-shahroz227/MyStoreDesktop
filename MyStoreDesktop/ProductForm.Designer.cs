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
            this.components = new System.ComponentModel.Container();
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
            this.picQRPreview = new System.Windows.Forms.PictureBox();
            this.btnGenerateQR = new System.Windows.Forms.Button();
            this.panelBarcode = new System.Windows.Forms.Panel();
            this.txtBarcodeValue = new System.Windows.Forms.TextBox();
            this.lblBarcodeInstruction = new System.Windows.Forms.Label();
            this.picBarcodePreview = new System.Windows.Forms.PictureBox();
            this.btnGenerateBarcode = new System.Windows.Forms.Button();
            this.txtCodeValue = new System.Windows.Forms.TextBox();
            this.lblCodeValue = new System.Windows.Forms.Label();
            this.cmbCodeType = new System.Windows.Forms.ComboBox();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.btnBrowseImage = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panelQRCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRPreview)).BeginInit();
            this.panelBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcodePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(41, 89);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(85, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(151, 83);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(292, 22);
            this.txtTitle.TabIndex = 1;
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(151, 27);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(196, 24);
            this.cboCategory.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(41, 33);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(62, 16);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(41, 139);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(58, 16);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(151, 133);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(292, 22);
            this.txtQuantity.TabIndex = 5;
            // 
            // lblSalePrice
            // 
            this.lblSalePrice.AutoSize = true;
            this.lblSalePrice.Location = new System.Drawing.Point(41, 188);
            this.lblSalePrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSalePrice.Name = "lblSalePrice";
            this.lblSalePrice.Size = new System.Drawing.Size(72, 16);
            this.lblSalePrice.TabIndex = 6;
            this.lblSalePrice.Text = "Sale Price:";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(151, 182);
            this.txtSalePrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(292, 22);
            this.txtSalePrice.TabIndex = 7;
            // 
            // lblPurchasePrice
            // 
            this.lblPurchasePrice.AutoSize = true;
            this.lblPurchasePrice.Location = new System.Drawing.Point(41, 237);
            this.lblPurchasePrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new System.Drawing.Size(101, 16);
            this.lblPurchasePrice.TabIndex = 8;
            this.lblPurchasePrice.Text = "Purchase Price:";
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.Location = new System.Drawing.Point(151, 231);
            this.txtPurchasePrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(292, 22);
            this.txtPurchasePrice.TabIndex = 9;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(41, 286);
            this.lblDiscount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(62, 16);
            this.lblDiscount.TabIndex = 10;
            this.lblDiscount.Text = "Discount:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(151, 280);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(292, 22);
            this.txtDiscount.TabIndex = 11;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(541, 41);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(68, 16);
            this.lblCompany.TabIndex = 12;
            this.lblCompany.Text = "Company:";
            // 
            // cboCompany
            // 
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(631, 33);
            this.cboCompany.Margin = new System.Windows.Forms.Padding(4);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(183, 24);
            this.cboCompany.TabIndex = 13;
            // 
            // btnManageCompanies
            // 
            this.btnManageCompanies.Location = new System.Drawing.Point(822, 23);
            this.btnManageCompanies.Margin = new System.Windows.Forms.Padding(4);
            this.btnManageCompanies.Name = "btnManageCompanies";
            this.btnManageCompanies.Size = new System.Drawing.Size(115, 42);
            this.btnManageCompanies.TabIndex = 14;
            this.btnManageCompanies.Text = "Manage";
            this.btnManageCompanies.UseVisualStyleBackColor = true;
            this.btnManageCompanies.Click += new System.EventHandler(this.btnManageCompanies_Click);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(539, 95);
            this.lblModel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(48, 16);
            this.lblModel.TabIndex = 14;
            this.lblModel.Text = "Model:";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(630, 88);
            this.txtModel.Margin = new System.Windows.Forms.Padding(4);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(292, 22);
            this.txtModel.TabIndex = 15;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(986, 331);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 43);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(1095, 331);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(101, 43);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Crimson;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(1204, 331);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(101, 43);
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
            this.dgvProducts.Location = new System.Drawing.Point(17, 399);
            this.dgvProducts.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(1487, 277);
            this.dgvProducts.TabIndex = 19;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            this.dgvProducts.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvProducts_KeyUp);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(630, 128);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(292, 22);
            this.txtDescription.TabIndex = 21;
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(539, 137);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(75, 16);
            this.lbDescription.TabIndex = 20;
            this.lbDescription.Text = "Description";
            // 
            // btnManageCategories
            // 
            this.btnManageCategories.BackColor = System.Drawing.Color.GhostWhite;
            this.btnManageCategories.ForeColor = System.Drawing.Color.Black;
            this.btnManageCategories.Location = new System.Drawing.Point(355, 20);
            this.btnManageCategories.Margin = new System.Windows.Forms.Padding(4);
            this.btnManageCategories.Name = "btnManageCategories";
            this.btnManageCategories.Size = new System.Drawing.Size(115, 42);
            this.btnManageCategories.TabIndex = 25;
            this.btnManageCategories.Text = "Manage Categories";
            this.btnManageCategories.UseVisualStyleBackColor = false;
            this.btnManageCategories.Click += new System.EventHandler(this.btnManageCategories_Click);
            // 
            // panelQRCode
            // 
            this.panelQRCode.Controls.Add(this.picQRPreview);
            this.panelQRCode.Controls.Add(this.btnGenerateQR);
            this.panelQRCode.Location = new System.Drawing.Point(1062, 115);
            this.panelQRCode.Margin = new System.Windows.Forms.Padding(4);
            this.panelQRCode.Name = "panelQRCode";
            this.panelQRCode.Size = new System.Drawing.Size(323, 127);
            this.panelQRCode.TabIndex = 26;
            this.panelQRCode.Visible = false;
            // 
            // picQRPreview
            // 
            this.picQRPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picQRPreview.Location = new System.Drawing.Point(197, 16);
            this.picQRPreview.Margin = new System.Windows.Forms.Padding(4);
            this.picQRPreview.Name = "picQRPreview";
            this.picQRPreview.Size = new System.Drawing.Size(105, 98);
            this.picQRPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQRPreview.TabIndex = 26;
            this.picQRPreview.TabStop = false;
            // 
            // btnGenerateQR
            // 
            this.btnGenerateQR.BackColor = System.Drawing.Color.SlateBlue;
            this.btnGenerateQR.ForeColor = System.Drawing.Color.White;
            this.btnGenerateQR.Location = new System.Drawing.Point(11, 42);
            this.btnGenerateQR.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateQR.Name = "btnGenerateQR";
            this.btnGenerateQR.Size = new System.Drawing.Size(160, 43);
            this.btnGenerateQR.TabIndex = 25;
            this.btnGenerateQR.Text = "Generate QR Code";
            this.btnGenerateQR.UseVisualStyleBackColor = false;
            this.btnGenerateQR.Click += new System.EventHandler(this.btnGenerateQR_Click);
            // 
            // panelBarcode
            // 
            this.panelBarcode.Controls.Add(this.txtBarcodeValue);
            this.panelBarcode.Controls.Add(this.lblBarcodeInstruction);
            this.panelBarcode.Controls.Add(this.picBarcodePreview);
            this.panelBarcode.Controls.Add(this.btnGenerateBarcode);
            this.panelBarcode.Location = new System.Drawing.Point(1496, 669);
            this.panelBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.panelBarcode.Name = "panelBarcode";
            this.panelBarcode.Size = new System.Drawing.Size(25, 19);
            this.panelBarcode.TabIndex = 28;
            this.panelBarcode.Visible = false;
            // 
            // txtBarcodeValue
            // 
            this.txtBarcodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcodeValue.Location = new System.Drawing.Point(8, 37);
            this.txtBarcodeValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarcodeValue.Name = "txtBarcodeValue";
            this.txtBarcodeValue.Size = new System.Drawing.Size(292, 26);
            this.txtBarcodeValue.TabIndex = 27;
            this.txtBarcodeValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcodeValue_KeyPress);
            // 
            // lblBarcodeInstruction
            // 
            this.lblBarcodeInstruction.AutoSize = true;
            this.lblBarcodeInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcodeInstruction.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblBarcodeInstruction.Location = new System.Drawing.Point(8, 12);
            this.lblBarcodeInstruction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBarcodeInstruction.Name = "lblBarcodeInstruction";
            this.lblBarcodeInstruction.Size = new System.Drawing.Size(238, 17);
            this.lblBarcodeInstruction.TabIndex = 28;
            this.lblBarcodeInstruction.Text = "Scan Barcode from Product Box";
            // 
            // picBarcodePreview
            // 
            this.picBarcodePreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBarcodePreview.Location = new System.Drawing.Point(8, 125);
            this.picBarcodePreview.Margin = new System.Windows.Forms.Padding(4);
            this.picBarcodePreview.Name = "picBarcodePreview";
            this.picBarcodePreview.Size = new System.Drawing.Size(292, 36);
            this.picBarcodePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBarcodePreview.TabIndex = 26;
            this.picBarcodePreview.TabStop = false;
            this.picBarcodePreview.Visible = false;
            // 
            // btnGenerateBarcode
            // 
            this.btnGenerateBarcode.BackColor = System.Drawing.Color.SlateBlue;
            this.btnGenerateBarcode.ForeColor = System.Drawing.Color.White;
            this.btnGenerateBarcode.Location = new System.Drawing.Point(8, 117);
            this.btnGenerateBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateBarcode.Name = "btnGenerateBarcode";
            this.btnGenerateBarcode.Size = new System.Drawing.Size(160, 43);
            this.btnGenerateBarcode.TabIndex = 25;
            this.btnGenerateBarcode.Text = "Generate Random";
            this.btnGenerateBarcode.UseVisualStyleBackColor = false;
            this.btnGenerateBarcode.Visible = false;
            this.btnGenerateBarcode.Click += new System.EventHandler(this.btnGenerateBarcode_Click);
            // 
            // txtCodeValue
            // 
            this.txtCodeValue.BackColor = System.Drawing.Color.LightGray;
            this.txtCodeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeValue.Location = new System.Drawing.Point(1129, 61);
            this.txtCodeValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodeValue.Name = "txtCodeValue";
            this.txtCodeValue.ReadOnly = true;
            this.txtCodeValue.Size = new System.Drawing.Size(229, 26);
            this.txtCodeValue.TabIndex = 33;
            // 
            // lblCodeValue
            // 
            this.lblCodeValue.AutoSize = true;
            this.lblCodeValue.Location = new System.Drawing.Point(1032, 67);
            this.lblCodeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeValue.Name = "lblCodeValue";
            this.lblCodeValue.Size = new System.Drawing.Size(81, 16);
            this.lblCodeValue.TabIndex = 34;
            this.lblCodeValue.Text = "Code Value:";
            // 
            // cmbCodeType
            // 
            this.cmbCodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCodeType.FormattingEnabled = true;
            this.cmbCodeType.Items.AddRange(new object[] {
            "QR Code",
            "Bar Code",
            "Manual Code"});
            this.cmbCodeType.Location = new System.Drawing.Point(1035, 28);
            this.cmbCodeType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCodeType.Name = "cmbCodeType";
            this.cmbCodeType.Size = new System.Drawing.Size(323, 24);
            this.cmbCodeType.TabIndex = 29;
            // 
            // picProduct
            // 
            this.picProduct.BackColor = System.Drawing.Color.White;
            this.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picProduct.Location = new System.Drawing.Point(719, 173);
            this.picProduct.Margin = new System.Windows.Forms.Padding(4);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(203, 191);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 30;
            this.picProduct.TabStop = false;
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Location = new System.Drawing.Point(544, 237);
            this.btnBrowseImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(115, 47);
            this.btnBrowseImage.TabIndex = 31;
            this.btnBrowseImage.Text = "Select Image";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.Crimson;
            this.btnClean.ForeColor = System.Drawing.Color.White;
            this.btnClean.Location = new System.Drawing.Point(1393, 23);
            this.btnClean.Margin = new System.Windows.Forms.Padding(4);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(114, 36);
            this.btnClean.TabIndex = 32;
            this.btnClean.Text = "CleanForm Data";
            this.btnClean.UseVisualStyleBackColor = false;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1520, 690);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.lblCodeValue);
            this.Controls.Add(this.panelBarcode);
            this.Controls.Add(this.txtCodeValue);
            this.Controls.Add(this.btnBrowseImage);
            this.Controls.Add(this.picProduct);
            this.Controls.Add(this.cmbCodeType);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Management";
            this.Load += new System.EventHandler(this.ProductForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panelQRCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQRPreview)).EndInit();
            this.panelBarcode.ResumeLayout(false);
            this.panelBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcodePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.PictureBox picBarcodePreview;
        private System.Windows.Forms.Panel panelBarcode;
        private System.Windows.Forms.TextBox txtBarcodeValue;
        private System.Windows.Forms.Label lblBarcodeInstruction;
        private System.Windows.Forms.Button btnGenerateBarcode;
        private System.Windows.Forms.TextBox txtCodeValue;
        private System.Windows.Forms.Label lblCodeValue;
        private System.Windows.Forms.ComboBox cmbCodeType;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}