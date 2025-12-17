namespace MyStoreDesktop
{
    partial class SettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbKey = new System.Windows.Forms.ComboBox();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SettingUpdateViewPanel = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtSUpdateValue = new System.Windows.Forms.TextBox();
            this.lblUpdateKey = new System.Windows.Forms.Label();
            this.UUVBtnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpdateSetting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.SettingUpdateViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(137, 92);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(25, 13);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Key";
            // 
            // cmbKey
            // 
            this.cmbKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKey.FormattingEnabled = true;
            this.cmbKey.Items.AddRange(new object[] {
            "Base Path",
            "Store Name"});
            this.cmbKey.Location = new System.Drawing.Point(255, 89);
            this.cmbKey.Name = "cmbKey";
            this.cmbKey.Size = new System.Drawing.Size(220, 21);
            this.cmbKey.TabIndex = 4;
            // 
            // dgvSettings
            // 
            this.dgvSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSettings.Location = new System.Drawing.Point(12, 214);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvSettings.Size = new System.Drawing.Size(776, 235);
            this.dgvSettings.TabIndex = 5;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(255, 130);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(220, 20);
            this.txtValue.TabIndex = 7;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(135, 135);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(34, 13);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(274, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 37);
            this.label1.TabIndex = 8;
            this.label1.Text = "Setting Form";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(290, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 40);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "➕ Add";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingUpdateViewPanel
            // 
            this.SettingUpdateViewPanel.Controls.Add(this.btnClose);
            this.SettingUpdateViewPanel.Controls.Add(this.txtSUpdateValue);
            this.SettingUpdateViewPanel.Controls.Add(this.lblUpdateKey);
            this.SettingUpdateViewPanel.Controls.Add(this.UUVBtnClose);
            this.SettingUpdateViewPanel.Controls.Add(this.label4);
            this.SettingUpdateViewPanel.Controls.Add(this.btnUpdateSetting);
            this.SettingUpdateViewPanel.Location = new System.Drawing.Point(11, 22);
            this.SettingUpdateViewPanel.Name = "SettingUpdateViewPanel";
            this.SettingUpdateViewPanel.Size = new System.Drawing.Size(777, 427);
            this.SettingUpdateViewPanel.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(708, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 37);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSUpdateValue
            // 
            this.txtSUpdateValue.Location = new System.Drawing.Point(286, 188);
            this.txtSUpdateValue.Name = "txtSUpdateValue";
            this.txtSUpdateValue.Size = new System.Drawing.Size(220, 20);
            this.txtSUpdateValue.TabIndex = 18;
            // 
            // lblUpdateKey
            // 
            this.lblUpdateKey.AutoSize = true;
            this.lblUpdateKey.Location = new System.Drawing.Point(166, 193);
            this.lblUpdateKey.Name = "lblUpdateKey";
            this.lblUpdateKey.Size = new System.Drawing.Size(34, 13);
            this.lblUpdateKey.TabIndex = 17;
            this.lblUpdateKey.Text = "Value";
            // 
            // UUVBtnClose
            // 
            this.UUVBtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.UUVBtnClose.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UUVBtnClose.ForeColor = System.Drawing.Color.White;
            this.UUVBtnClose.Location = new System.Drawing.Point(946, 3);
            this.UUVBtnClose.Name = "UUVBtnClose";
            this.UUVBtnClose.Size = new System.Drawing.Size(66, 37);
            this.UUVBtnClose.TabIndex = 14;
            this.UUVBtnClose.Text = "x";
            this.UUVBtnClose.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(288, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 37);
            this.label4.TabIndex = 13;
            this.label4.Text = "Setting Update";
            // 
            // btnUpdateSetting
            // 
            this.btnUpdateSetting.BackColor = System.Drawing.Color.OrangeRed;
            this.btnUpdateSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateSetting.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSetting.ForeColor = System.Drawing.Color.White;
            this.btnUpdateSetting.Location = new System.Drawing.Point(289, 246);
            this.btnUpdateSetting.Name = "btnUpdateSetting";
            this.btnUpdateSetting.Size = new System.Drawing.Size(140, 40);
            this.btnUpdateSetting.TabIndex = 8;
            this.btnUpdateSetting.Text = "✎ Update";
            this.btnUpdateSetting.UseVisualStyleBackColor = false;
            this.btnUpdateSetting.Click += new System.EventHandler(this.SettingbtnUpdate_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SettingUpdateViewPanel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvSettings);
            this.Controls.Add(this.cmbKey);
            this.Controls.Add(this.lblCategory);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.SettingUpdateViewPanel.ResumeLayout(false);
            this.SettingUpdateViewPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbKey;
        private System.Windows.Forms.DataGridView dgvSettings;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel SettingUpdateViewPanel;
        private System.Windows.Forms.TextBox txtSUpdateValue;
        private System.Windows.Forms.Label lblUpdateKey;
        private System.Windows.Forms.Button UUVBtnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpdateSetting;
        private System.Windows.Forms.Button btnClose;
    }
}