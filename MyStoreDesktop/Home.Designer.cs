using System;

namespace MyStoreDesktop
{
    partial class Home
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.Reports = new System.Windows.Forms.Button();
            this.Sales = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.txtChange = new System.Windows.Forms.TextBox();
            this.Change = new System.Windows.Forms.Label();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.Payment = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.TextBox();
            this.lblDiscountValue = new System.Windows.Forms.TextBox();
            this.lblSubtotalValue = new System.Windows.Forms.TextBox();
            this.txtTaxValue = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnNum9 = new System.Windows.Forms.Button();
            this.btnNum8 = new System.Windows.Forms.Button();
            this.btnNum7 = new System.Windows.Forms.Button();
            this.btnNum6 = new System.Windows.Forms.Button();
            this.btnNum5 = new System.Windows.Forms.Button();
            this.btnNum4 = new System.Windows.Forms.Button();
            this.btnNum3 = new System.Windows.Forms.Button();
            this.btnNum2 = new System.Windows.Forms.Button();
            this.btnNum1 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnlogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAddToCard = new System.Windows.Forms.DataGridView();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstSuggestion = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelMenu.SuspendLayout();
            this.panelMainContent.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddToCard)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.panelMenu.Controls.Add(this.Reports);
            this.panelMenu.Controls.Add(this.Sales);
            this.panelMenu.Controls.Add(this.btnUsers);
            this.panelMenu.Controls.Add(this.btnProducts);
            this.panelMenu.Controls.Add(this.btnHome);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(202, 661);
            this.panelMenu.TabIndex = 0;
            // 
            // Reports
            // 
            this.Reports.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Reports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reports.ForeColor = System.Drawing.Color.White;
            this.Reports.Location = new System.Drawing.Point(27, 340);
            this.Reports.Name = "Reports";
            this.Reports.Size = new System.Drawing.Size(129, 40);
            this.Reports.TabIndex = 4;
            this.Reports.Text = "Reports";
            this.Reports.UseVisualStyleBackColor = false;
            this.Reports.Click += new System.EventHandler(this.LoginPanelReports);
            // 
            // Sales
            // 
            this.Sales.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Sales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Sales.ForeColor = System.Drawing.Color.White;
            this.Sales.Location = new System.Drawing.Point(27, 266);
            this.Sales.Name = "Sales";
            this.Sales.Size = new System.Drawing.Size(129, 40);
            this.Sales.TabIndex = 3;
            this.Sales.Text = "Sales";
            this.Sales.UseVisualStyleBackColor = false;
            this.Sales.Click += new System.EventHandler(this.LoginPanelSales);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.ForeColor = System.Drawing.Color.White;
            this.btnUsers.Location = new System.Drawing.Point(27, 191);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(129, 40);
            this.btnUsers.TabIndex = 2;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.LoginPanelUsers);
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.Location = new System.Drawing.Point(27, 124);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(129, 40);
            this.btnProducts.TabIndex = 1;
            this.btnProducts.Text = "Products";
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.LoginPanelProduct);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Location = new System.Drawing.Point(27, 57);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(129, 40);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.LoginPanelbtnHome);
            // 
            // panelMainContent
            // 
            this.panelMainContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMainContent.Controls.Add(this.rightPanel);
            this.panelMainContent.Controls.Add(this.panel1);
            this.panelMainContent.Controls.Add(this.dgvAddToCard);
            this.panelMainContent.Controls.Add(this.lstSuggestion);
            this.panelMainContent.Controls.Add(this.btnSearch);
            this.panelMainContent.Controls.Add(this.txtSearch);
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.ForeColor = System.Drawing.Color.White;
            this.panelMainContent.Location = new System.Drawing.Point(202, 0);
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.Size = new System.Drawing.Size(982, 661);
            this.panelMainContent.TabIndex = 1;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.txtChange);
            this.rightPanel.Controls.Add(this.Change);
            this.rightPanel.Controls.Add(this.txtPayment);
            this.rightPanel.Controls.Add(this.Payment);
            this.rightPanel.Controls.Add(this.lblTotalValue);
            this.rightPanel.Controls.Add(this.lblDiscountValue);
            this.rightPanel.Controls.Add(this.lblSubtotalValue);
            this.rightPanel.Controls.Add(this.txtTaxValue);
            this.rightPanel.Controls.Add(this.btnConfirm);
            this.rightPanel.Controls.Add(this.btnNum9);
            this.rightPanel.Controls.Add(this.btnNum8);
            this.rightPanel.Controls.Add(this.btnNum7);
            this.rightPanel.Controls.Add(this.btnNum6);
            this.rightPanel.Controls.Add(this.btnNum5);
            this.rightPanel.Controls.Add(this.btnNum4);
            this.rightPanel.Controls.Add(this.btnNum3);
            this.rightPanel.Controls.Add(this.btnNum2);
            this.rightPanel.Controls.Add(this.btnNum1);
            this.rightPanel.Controls.Add(this.btnClear);
            this.rightPanel.Controls.Add(this.btnDot);
            this.rightPanel.Controls.Add(this.btnZero);
            this.rightPanel.Controls.Add(this.lblDiscount);
            this.rightPanel.Controls.Add(this.lblTotal);
            this.rightPanel.Controls.Add(this.lblTax);
            this.rightPanel.Controls.Add(this.lblSubTotal);
            this.rightPanel.Location = new System.Drawing.Point(660, 78);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(288, 549);
            this.rightPanel.TabIndex = 5;
            // 
            // txtChange
            // 
            this.txtChange.Location = new System.Drawing.Point(178, 186);
            this.txtChange.Name = "txtChange";
            this.txtChange.Size = new System.Drawing.Size(84, 20);
            this.txtChange.TabIndex = 28;
            // 
            // Change
            // 
            this.Change.AutoSize = true;
            this.Change.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Change.ForeColor = System.Drawing.Color.ForestGreen;
            this.Change.Location = new System.Drawing.Point(92, 188);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(65, 18);
            this.Change.TabIndex = 27;
            this.Change.Text = "Change";
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(178, 151);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(84, 20);
            this.txtPayment.TabIndex = 26;
            this.txtPayment.Click += new System.EventHandler(this.txtPayment_Click);
            this.txtPayment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayment_KeyDown);
            // 
            // Payment
            // 
            this.Payment.AutoSize = true;
            this.Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Payment.ForeColor = System.Drawing.Color.IndianRed;
            this.Payment.Location = new System.Drawing.Point(92, 153);
            this.Payment.Name = "Payment";
            this.Payment.Size = new System.Drawing.Size(73, 18);
            this.Payment.TabIndex = 25;
            this.Payment.Text = "Payment";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Location = new System.Drawing.Point(177, 121);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(84, 20);
            this.lblTotalValue.TabIndex = 24;
            this.lblTotalValue.Click += new System.EventHandler(this.lblTotalValue_Click);
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Location = new System.Drawing.Point(177, 56);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(84, 20);
            this.lblDiscountValue.TabIndex = 23;
            this.lblDiscountValue.Click += new System.EventHandler(this.lblDiscountValue_Click);
            this.lblDiscountValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lblDiscountValue_KeyDown);
            // 
            // lblSubtotalValue
            // 
            this.lblSubtotalValue.Location = new System.Drawing.Point(177, 24);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(84, 20);
            this.lblSubtotalValue.TabIndex = 22;
            // 
            // txtTaxValue
            // 
            this.txtTaxValue.Location = new System.Drawing.Point(177, 87);
            this.txtTaxValue.Name = "txtTaxValue";
            this.txtTaxValue.Size = new System.Drawing.Size(84, 20);
            this.txtTaxValue.TabIndex = 21;
            this.txtTaxValue.Click += new System.EventHandler(this.txtTaxValue_Click);
            this.txtTaxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaxValue_KeyDown);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.ForestGreen;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Transparent;
            this.btnConfirm.Location = new System.Drawing.Point(41, 468);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(220, 49);
            this.btnConfirm.TabIndex = 20;
            this.btnConfirm.Text = "Submit";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.BillConfirm_Click);
            // 
            // btnNum9
            // 
            this.btnNum9.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum9.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum9.Location = new System.Drawing.Point(207, 239);
            this.btnNum9.Name = "btnNum9";
            this.btnNum9.Size = new System.Drawing.Size(63, 33);
            this.btnNum9.TabIndex = 19;
            this.btnNum9.Text = "9";
            this.btnNum9.UseVisualStyleBackColor = false;
            this.btnNum9.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum8
            // 
            this.btnNum8.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum8.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum8.Location = new System.Drawing.Point(119, 239);
            this.btnNum8.Name = "btnNum8";
            this.btnNum8.Size = new System.Drawing.Size(63, 33);
            this.btnNum8.TabIndex = 18;
            this.btnNum8.Text = "8";
            this.btnNum8.UseVisualStyleBackColor = false;
            this.btnNum8.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum7
            // 
            this.btnNum7.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum7.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum7.Location = new System.Drawing.Point(32, 239);
            this.btnNum7.Name = "btnNum7";
            this.btnNum7.Size = new System.Drawing.Size(63, 33);
            this.btnNum7.TabIndex = 17;
            this.btnNum7.Text = "7";
            this.btnNum7.UseVisualStyleBackColor = false;
            this.btnNum7.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum6
            // 
            this.btnNum6.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum6.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum6.Location = new System.Drawing.Point(207, 292);
            this.btnNum6.Name = "btnNum6";
            this.btnNum6.Size = new System.Drawing.Size(63, 33);
            this.btnNum6.TabIndex = 16;
            this.btnNum6.Text = "6";
            this.btnNum6.UseVisualStyleBackColor = false;
            this.btnNum6.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum5
            // 
            this.btnNum5.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum5.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum5.Location = new System.Drawing.Point(119, 292);
            this.btnNum5.Name = "btnNum5";
            this.btnNum5.Size = new System.Drawing.Size(63, 33);
            this.btnNum5.TabIndex = 15;
            this.btnNum5.Text = "5";
            this.btnNum5.UseVisualStyleBackColor = false;
            this.btnNum5.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum4
            // 
            this.btnNum4.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum4.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum4.Location = new System.Drawing.Point(32, 292);
            this.btnNum4.Name = "btnNum4";
            this.btnNum4.Size = new System.Drawing.Size(63, 33);
            this.btnNum4.TabIndex = 14;
            this.btnNum4.Text = "4";
            this.btnNum4.UseVisualStyleBackColor = false;
            this.btnNum4.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum3
            // 
            this.btnNum3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum3.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum3.Location = new System.Drawing.Point(207, 348);
            this.btnNum3.Name = "btnNum3";
            this.btnNum3.Size = new System.Drawing.Size(63, 33);
            this.btnNum3.TabIndex = 13;
            this.btnNum3.Text = "3";
            this.btnNum3.UseVisualStyleBackColor = false;
            this.btnNum3.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum2
            // 
            this.btnNum2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum2.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum2.Location = new System.Drawing.Point(119, 348);
            this.btnNum2.Name = "btnNum2";
            this.btnNum2.Size = new System.Drawing.Size(63, 33);
            this.btnNum2.TabIndex = 12;
            this.btnNum2.Text = "2";
            this.btnNum2.UseVisualStyleBackColor = false;
            this.btnNum2.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnNum1
            // 
            this.btnNum1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnNum1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNum1.ForeColor = System.Drawing.Color.Transparent;
            this.btnNum1.Location = new System.Drawing.Point(32, 348);
            this.btnNum1.Name = "btnNum1";
            this.btnNum1.Size = new System.Drawing.Size(63, 33);
            this.btnNum1.TabIndex = 11;
            this.btnNum1.Text = "1";
            this.btnNum1.UseVisualStyleBackColor = false;
            this.btnNum1.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Transparent;
            this.btnClear.Location = new System.Drawing.Point(207, 405);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(63, 33);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDot
            // 
            this.btnDot.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnDot.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDot.ForeColor = System.Drawing.Color.Transparent;
            this.btnDot.Location = new System.Drawing.Point(119, 405);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(63, 33);
            this.btnDot.TabIndex = 9;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = false;
            this.btnDot.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // btnZero
            // 
            this.btnZero.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnZero.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZero.ForeColor = System.Drawing.Color.Transparent;
            this.btnZero.Location = new System.Drawing.Point(32, 405);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(63, 33);
            this.btnZero.TabIndex = 8;
            this.btnZero.Text = "0";
            this.btnZero.UseVisualStyleBackColor = false;
            this.btnZero.Click += new System.EventHandler(this.NumberButton_Click);
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.ForeColor = System.Drawing.Color.Black;
            this.lblDiscount.Location = new System.Drawing.Point(91, 25);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(64, 16);
            this.lblDiscount.TabIndex = 6;
            this.lblDiscount.Text = "Subtotal";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(91, 123);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(46, 18);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.ForeColor = System.Drawing.Color.Black;
            this.lblTax.Location = new System.Drawing.Point(91, 92);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(50, 16);
            this.lblTax.TabIndex = 4;
            this.lblTax.Text = "Tax %";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.Black;
            this.lblSubTotal.Location = new System.Drawing.Point(91, 56);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(67, 16);
            this.lblSubTotal.TabIndex = 3;
            this.lblSubTotal.Text = "Discount";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Tan;
            this.panel1.Controls.Add(this.btnlogout);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 59);
            this.panel1.TabIndex = 4;
            // 
            // btnlogout
            // 
            this.btnlogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnlogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogout.Location = new System.Drawing.Point(865, 15);
            this.btnlogout.Name = "btnlogout";
            this.btnlogout.Size = new System.Drawing.Size(102, 36);
            this.btnlogout.TabIndex = 1;
            this.btnlogout.Text = "LOGOUT";
            this.btnlogout.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "MY STORE";
            // 
            // dgvAddToCard
            // 
            this.dgvAddToCard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAddToCard.BackgroundColor = System.Drawing.Color.White;
            this.dgvAddToCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAddToCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductId,
            this.Title,
            this.Quantity,
            this.SalePrice,
            this.Discount,
            this.Total});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAddToCard.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAddToCard.GridColor = System.Drawing.Color.LightGray;
            this.dgvAddToCard.Location = new System.Drawing.Point(6, 127);
            this.dgvAddToCard.Name = "dgvAddToCard";
            this.dgvAddToCard.Size = new System.Drawing.Size(648, 503);
            this.dgvAddToCard.TabIndex = 3;
            this.dgvAddToCard.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAddToCard_CellEndEdit);
            this.dgvAddToCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvAddToCard_KeyDown);
            // 
            // ProductId
            // 
            this.ProductId.HeaderText = "Product ID";
            this.ProductId.Name = "ProductId";
            this.ProductId.Visible = false;
            // 
            // Title
            // 
            this.Title.HeaderText = "ProductName";
            this.Title.Name = "Title";
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SalePrice
            // 
            this.SalePrice.HeaderText = "ItemPrice";
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            // 
            // Total
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.Total.DefaultCellStyle = dataGridViewCellStyle1;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lstSuggestion
            // 
            this.lstSuggestion.FormattingEnabled = true;
            this.lstSuggestion.Location = new System.Drawing.Point(6, 102);
            this.lstSuggestion.Name = "lstSuggestion";
            this.lstSuggestion.Size = new System.Drawing.Size(542, 108);
            this.lstSuggestion.TabIndex = 2;
            this.lstSuggestion.Visible = false;
            this.lstSuggestion.Click += new System.EventHandler(this.lstSuggestion_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.RosyBrown;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(554, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(84, 32);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 68);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(542, 28);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.panelMenu.ResumeLayout(false);
            this.panelMainContent.ResumeLayout(false);
            this.panelMainContent.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAddToCard)).EndInit();
            this.ResumeLayout(false);

        }

        private void lstSuggestion_click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel panelMainContent;
        private System.Windows.Forms.Button Reports;
        private System.Windows.Forms.Button Sales;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.ListBox lstSuggestion;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvAddToCard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnlogout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnNum1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnNum2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnNum9;
        private System.Windows.Forms.Button btnNum8;
        private System.Windows.Forms.Button btnNum7;
        private System.Windows.Forms.Button btnNum6;
        private System.Windows.Forms.Button btnNum5;
        private System.Windows.Forms.Button btnNum4;
        private System.Windows.Forms.Button btnNum3;
        private System.Windows.Forms.TextBox txtTaxValue;
        private System.Windows.Forms.TextBox lblSubtotalValue;
        private System.Windows.Forms.TextBox lblTotalValue;
        private System.Windows.Forms.TextBox lblDiscountValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.TextBox txtChange;
        private System.Windows.Forms.Label Change;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Label Payment;
    }
}