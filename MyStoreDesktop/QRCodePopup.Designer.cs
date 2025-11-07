namespace MyStoreDesktop
{
    partial class QRCodePopup
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
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.btnPrintQR = new System.Windows.Forms.Button();
            this.btnAddToProduct = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            //
            // picQRCode
            //
            this.picQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQRCode.Location = new System.Drawing.Point(55, 50);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(290, 290);
            this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picQRCode.TabIndex = 0;
            this.picQRCode.TabStop = false;
            //
            // btnPrintQR
            //
            this.btnPrintQR.BackColor = System.Drawing.Color.SeaGreen;
            this.btnPrintQR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintQR.ForeColor = System.Drawing.Color.White;
            this.btnPrintQR.Location = new System.Drawing.Point(35, 360);
            this.btnPrintQR.Name = "btnPrintQR";
            this.btnPrintQR.Size = new System.Drawing.Size(140, 40);
            this.btnPrintQR.TabIndex = 1;
            this.btnPrintQR.Text = "Print QR Code";
            this.btnPrintQR.UseVisualStyleBackColor = false;
            this.btnPrintQR.Click += new System.EventHandler(this.btnPrintQR_Click);
            //
            // btnAddToProduct
            //
            this.btnAddToProduct.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddToProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddToProduct.Location = new System.Drawing.Point(225, 360);
            this.btnAddToProduct.Name = "btnAddToProduct";
            this.btnAddToProduct.Size = new System.Drawing.Size(140, 40);
            this.btnAddToProduct.TabIndex = 2;
            this.btnAddToProduct.Text = "Add To Product";
            this.btnAddToProduct.UseVisualStyleBackColor = false;
            this.btnAddToProduct.Click += new System.EventHandler(this.btnAddToProduct_Click);
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTitle.Location = new System.Drawing.Point(85, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(230, 25);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Generated QR Code";
            //
            // QRCodePopup
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(400, 420);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddToProduct);
            this.Controls.Add(this.btnPrintQR);
            this.Controls.Add(this.picQRCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QRCodePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR Code Generator";
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Button btnPrintQR;
        private System.Windows.Forms.Button btnAddToProduct;
        private System.Windows.Forms.Label lblTitle;
    }
}
