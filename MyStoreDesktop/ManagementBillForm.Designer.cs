namespace MyStoreDesktop.Forms
{
    partial class ManagementBillForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBillID;
        private System.Windows.Forms.Button btnSearchBill;
        private System.Windows.Forms.DataGridView dgvBillProducts;
        private System.Windows.Forms.Label lblGrandTotal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtBillID = new System.Windows.Forms.TextBox();
            this.btnSearchBill = new System.Windows.Forms.Button();
            this.dgvBillProducts = new System.Windows.Forms.DataGridView();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.btnViewReturnHistoryForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBillID
            // 
            this.txtBillID.Location = new System.Drawing.Point(23, 12);
            this.txtBillID.Name = "txtBillID";
            this.txtBillID.Size = new System.Drawing.Size(193, 20);
            this.txtBillID.TabIndex = 0;
            // 
            // btnSearchBill
            // 
            this.btnSearchBill.Location = new System.Drawing.Point(229, 10);
            this.btnSearchBill.Name = "btnSearchBill";
            this.btnSearchBill.Size = new System.Drawing.Size(100, 34);
            this.btnSearchBill.TabIndex = 1;
            this.btnSearchBill.Text = "Search";
            this.btnSearchBill.UseVisualStyleBackColor = true;
            this.btnSearchBill.Click += new System.EventHandler(this.btnSearchBill_Click);
            // 
            // dgvBillProducts
            // 
            this.dgvBillProducts.AllowUserToAddRows = false;
            this.dgvBillProducts.AllowUserToDeleteRows = false;
            this.dgvBillProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillProducts.Location = new System.Drawing.Point(12, 50);
            this.dgvBillProducts.Name = "dgvBillProducts";
            this.dgvBillProducts.ReadOnly = true;
            this.dgvBillProducts.RowHeadersWidth = 51;
            this.dgvBillProducts.RowTemplate.Height = 24;
            this.dgvBillProducts.Size = new System.Drawing.Size(760, 350);
            this.dgvBillProducts.TabIndex = 2;
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Location = new System.Drawing.Point(12, 410);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(69, 13);
            this.lblGrandTotal.TabIndex = 3;
            this.lblGrandTotal.Text = "Grand Total: ";
            // 
            // btnViewReturnHistoryForm
            // 
            this.btnViewReturnHistoryForm.Location = new System.Drawing.Point(628, 10);
            this.btnViewReturnHistoryForm.Name = "btnViewReturnHistoryForm";
            this.btnViewReturnHistoryForm.Size = new System.Drawing.Size(111, 30);
            this.btnViewReturnHistoryForm.TabIndex = 4;
            this.btnViewReturnHistoryForm.Text = "View Histry";
            this.btnViewReturnHistoryForm.UseVisualStyleBackColor = true;
            this.btnViewReturnHistoryForm.Click += new System.EventHandler(this.btnViewReturnHistoryForm_Click);
            // 
            // ManagementBillForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.btnViewReturnHistoryForm);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.dgvBillProducts);
            this.Controls.Add(this.btnSearchBill);
            this.Controls.Add(this.txtBillID);
            this.Name = "ManagementBillForm";
            this.Text = "Management Bill";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnViewReturnHistoryForm;
    }
}
