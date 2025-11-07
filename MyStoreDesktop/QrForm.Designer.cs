namespace MyStoreDesktop
{
    partial class QrForm
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
            this.lblProduct = new System.Windows.Forms.Label();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.lblQrText = new System.Windows.Forms.Label();
            this.txtQrCode = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvQrCodes = new System.Windows.Forms.DataGridView();
            this.picQr = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQrCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQr)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 30);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(269, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📦 QR Code Generator";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(50, 90);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(107, 20);
            this.lblProduct.TabIndex = 1;
            this.lblProduct.Text = "Select Product:";
            // 
            // cmbProducts
            // 
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(200, 85);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(300, 25);
            this.cmbProducts.TabIndex = 2;
            // 
            // lblQrText
            // 
            this.lblQrText.AutoSize = true;
            this.lblQrText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQrText.Location = new System.Drawing.Point(50, 140);
            this.lblQrText.Name = "lblQrText";
            this.lblQrText.Size = new System.Drawing.Size(102, 20);
            this.lblQrText.TabIndex = 3;
            this.lblQrText.Text = "QR Code Text:";
            // 
            // txtQrCode
            // 
            this.txtQrCode.Location = new System.Drawing.Point(200, 135);
            this.txtQrCode.Name = "txtQrCode";
            this.txtQrCode.ReadOnly = true;
            this.txtQrCode.Size = new System.Drawing.Size(300, 25);
            this.txtQrCode.TabIndex = 4;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(200, 190);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 40);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate QR";
            this.btnGenerate.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(370, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save QR";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // dgvQrCodes
            // 
            this.dgvQrCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQrCodes.BackgroundColor = System.Drawing.Color.White;
            this.dgvQrCodes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvQrCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQrCodes.Location = new System.Drawing.Point(30, 370);
            this.dgvQrCodes.Name = "dgvQrCodes";
            this.dgvQrCodes.ReadOnly = true;
            this.dgvQrCodes.RowHeadersVisible = false;
            this.dgvQrCodes.Size = new System.Drawing.Size(820, 200);
            this.dgvQrCodes.TabIndex = 8;
            // 
            // picQr
            // 
            this.picQr.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picQr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQr.Location = new System.Drawing.Point(550, 85);
            this.picQr.Name = "picQr";
            this.picQr.Size = new System.Drawing.Size(250, 250);
            this.picQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picQr.TabIndex = 7;
            this.picQr.TabStop = false;
            // 
            // QrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.dgvQrCodes);
            this.Controls.Add(this.picQr);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtQrCode);
            this.Controls.Add(this.lblQrText);
            this.Controls.Add(this.cmbProducts);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "QrForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QR Code Generator";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQrCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.Label lblQrText;
        private System.Windows.Forms.TextBox txtQrCode;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox picQr;
        private System.Windows.Forms.DataGridView dgvQrCodes;
    }
}