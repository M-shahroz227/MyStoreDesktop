using MyStoreDesktop.Data;
using MyStoreDesktop.Models;
using MyStoreDesktop.Services;
using MyStoreDesktop.Services.BarcodeReaderService;
using MyStoreDesktop.Services.BillProductService;
using MyStoreDesktop.Services.BillService;
using MyStoreDesktop.Services.CustomerInvoiceService;
using MyStoreDesktop.Services.FileServices;
using MyStoreDesktop.Services.ProductService;
using MyStoreDesktop.Theme;
using POSApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class Home : Form
    {
        // Services
        private readonly ProductService _productService = new ProductService();
        private readonly BillService _billService = new BillService();
        private readonly BillProductService _billProductService = new BillProductService();
        private readonly CustomerInvoiceService _customerService = new CustomerInvoiceService();
        private readonly FileServices _fileServices = new FileServices();
        private readonly SettingService _settingService = new SettingService();
        private readonly BarcodeReaderService _barcodeReader = new BarcodeReaderService();
        private readonly FrmGoogleDriveBackup _frmGoogleDriveBackup = new FrmGoogleDriveBackup();



        // Value holders
        private double _subtotal = 0;
        private double _discountPercent = 0;
        private double _sideDiscount = 0; 

        private double _taxPercent = 0;
        private TextBox _selectedBox = null;
        private Timer blinkTimer = new Timer();
        private bool isBlinking = false;
        private int _suggestionIndex = -1;

        // Tab Management
        private BillTabManager _tabManager;
        private BillSessionState _currentSession;
        private TabPage _plusTab;
        private TabPage _previousTab;







        public Home()
        {
            InitializeComponent();
            LoadHeaderName();

            // Apply professional blue theme
            ThemeManager.ApplyTheme(this);
            ApplyRoleAccess();

            txtSearch.TabIndex = 0;
            dgvAddToCard.TabIndex = 1;
            txtPayment.TabIndex = 2;
            btnConfirm.TabIndex = 3;



            lstSuggestion.Visible = false;
            dgvAddToCard.CellContentClick += dgvAddToCard_CellContentClick;
            dgvAddToCard.CellClick += dgvAddToCard_CellClick;
            txtPayment.TextChanged += txtPayment_TextChanged;
            txtTaxValue.Text = "0";   // ⭐ Default TAX 0
            lblDiscountValue.Text = "0"; // 
            lblDiscountValue.ReadOnly = true;

            dgvAddToCard.Columns["SalePrice"].ReadOnly = false;
            btnConfirm.Enabled = false;



            SetupGridButtons();
            dgvAddToCard.ColumnHeadersVisible = true;
            dgvAddToCard.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvAddToCard.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvAddToCard.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvAddToCard.DefaultCellStyle.ForeColor = Color.Black;
            dgvAddToCard.DefaultCellStyle.BackColor = Color.White;
            dgvAddToCard.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            dgvAddToCard.CellContentClick += dgvAddToCard_CellContentClick;
            // ✅ Hidden Image Column for Product Preview
            if (!dgvAddToCard.Columns.Contains("UrlImage"))
            {
                dgvAddToCard.Columns.Add("UrlImage", "UrlImage");
                dgvAddToCard.Columns["UrlImage"].Visible = false;
            }

            // ✅ Selection Event for Image Preview
            dgvAddToCard.SelectionChanged += dgvAddToCard_SelectionChanged;

            // Initialize Tab Control for Multi-Bill
            InitializeTabControl();

            // Wire up Load event for setting focus after form is fully loaded
            this.Load += Home_Load_SetFocus;

            // Setup barcode scanner event handler
            _barcodeReader.BarcodeScanned += BarcodeReader_BarcodeScanned;

        }

        // ================= GRID SETUP =================
        private void SetupGridButtons()
        {
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "Delete";
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
            deleteButton.UseColumnTextForButtonValue = true;

            if (!dgvAddToCard.Columns.Contains("Delete"))
                dgvAddToCard.Columns.Add(deleteButton);
        }

        // ================= TAB MANAGEMENT =================
        private void InitializeTabControl()
        {
            _tabManager = new BillTabManager();

            // Hide original controls (they'll be recreated in tabs)
            txtSearch.Visible = false;
            btnSearch.Visible = false;
            lstSuggestion.Visible = false;
            dgvAddToCard.Visible = false;

            // Configure TabControl (already added in Designer.cs)
            tabControlBills.Location = new Point(6, 62);
            tabControlBills.Size = new Size(691, 518);
            tabControlBills.Dock = DockStyle.None;
            tabControlBills.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlBills.BringToFront();
            tabControlBills.Visible = true;

            // Create first tab WITHOUT events wired (to avoid initialization issues)
            var firstTab = CreateNewBillTab();
            _previousTab = firstTab;

            // Create "+" tab
            CreatePlusTab();

            // NOW wire up events after tabs are created
            tabControlBills.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            tabControlBills.MouseClick += TabControl_MouseClick;
            tabControlBills.MouseDown += TabControl_MouseDown;

            // Ensure first tab is selected and load its state
            tabControlBills.SelectedIndex = 0;
            tabControlBills.SelectedTab = firstTab;
            LoadSessionState(firstTab);

            // Store reference to first tab for later focus setting
            _firstTabForFocus = firstTab;
        }

        private TabPage _firstTabForFocus;

        private void Home_Load_SetFocus(object sender, EventArgs e)
        {
            // Set focus after the form is fully loaded
            if (_firstTabForFocus != null)
            {
                var firstTabControls = GetTabControls(_firstTabForFocus);
                if (firstTabControls != null && firstTabControls.SearchBox != null)
                {
                    firstTabControls.SearchBox.Focus();
                }
                _firstTabForFocus = null; // Clear the reference
            }
        }

        private TabPage CreateNewBillTab()
        {
            if (!_tabManager.CanAddMoreTabs())
            {
                MessageBox.Show("Maximum 5 bills allowed simultaneously.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            // Create session state
            var session = _tabManager.CreateNewSession();

            // Create tab page
            TabPage tab = new TabPage($"Bill #{session.TabNumber}");
            tab.Visible = true;
            tab.UseVisualStyleBackColor = true;

            // Create tab content (search box, grid, etc.)
            CreateTabContent(tab, session);

            // Register with manager
            _tabManager.RegisterTab(tab, session);

            // Add to TabControl (before "+" tab)
            if (_plusTab != null && tabControlBills.TabPages.Contains(_plusTab))
            {
                int plusIndex = tabControlBills.TabPages.IndexOf(_plusTab);
                tabControlBills.TabPages.Insert(plusIndex, tab);
            }
            else
            {
                // No "+" tab yet, just add it
                tabControlBills.TabPages.Add(tab);
            }

            return tab;
        }

        private void CreateTabContent(TabPage tab, BillSessionState session)
        {
            // Ensure the tab page is set up properly
            tab.UseVisualStyleBackColor = true;
            tab.BackColor = Color.WhiteSmoke;

            // Create search textbox
            TextBox txtSearchTab = new TextBox
            {
                Name = "txtSearch",
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Location = new Point(9, 11),
                Size = new Size(591, 25),
                Multiline = false,
                TabIndex = 0,
                Visible = true
            };
            txtSearchTab.TextChanged += txtSearch_TextChanged;
            txtSearchTab.KeyDown += txtSearch_KeyDown;
            txtSearchTab.KeyPress += txtSearch_KeyPress;  // For barcode scanner
            txtSearchTab.Enter += txtSearch_Enter;
            txtSearchTab.Leave += txtSearch_Leave;
            tab.Controls.Add(txtSearchTab);

            // Create search button
            Button btnSearchTab = new Button
            {
                Name = "btnSearch",
                Text = "Search",
                BackColor = Color.RosyBrown,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(605, 8),
                Size = new Size(84, 32),
                TabIndex = 1,
                Visible = true
            };
            tab.Controls.Add(btnSearchTab);

            // Create suggestion listbox
            ListBox lstSuggestionTab = new ListBox
            {
                Name = "lstSuggestion",
                Location = new Point(8, 42),
                Size = new Size(591, 108),
                Visible = false,
                TabIndex = 2
            };
            lstSuggestionTab.Click += lstSuggestion_Click;
            tab.Controls.Add(lstSuggestionTab);

            // Create DataGridView
            DataGridView dgv = CreateShoppingCartGrid();
            dgv.Location = new Point(6, 67);
            dgv.Size = new Size(691, 420);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv.TabIndex = 3;
            dgv.Visible = true;
            tab.Controls.Add(dgv);

            // Store control references in tab's Tag
            var controls = new BillTabControls
            {
                SearchBox = txtSearchTab,
                SearchButton = btnSearchTab,
                SuggestionList = lstSuggestionTab,
                CartGrid = dgv,
                Session = session
            };
            tab.Tag = controls;
        }

        private DataGridView CreateShoppingCartGrid()
        {
            DataGridView dgv = new DataGridView
            {
                Name = "dgvAddToCard",
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.Raised,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                GridColor = Color.LightGray
            };

            // Add columns
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                HeaderText = "Product ID",
                Visible = false
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                HeaderText = "ProductName"
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity"
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SalePrice",
                HeaderText = "ItemPrice",
                ReadOnly = false
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Discount",
                HeaderText = "Discount"
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total"
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UrlImage",
                HeaderText = "UrlImage",
                Visible = false
            });

            // Add Delete button column
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };
            deleteButton.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
            dgv.Columns.Add(deleteButton);

            // Wire up events
            dgv.CellEndEdit += dgvAddToCard_CellEndEdit;
            dgv.CellClick += dgvAddToCard_CellClick;
            dgv.CellContentClick += dgvAddToCard_CellContentClick;
            dgv.SelectionChanged += dgvAddToCard_SelectionChanged;
            dgv.KeyDown += dgvAddToCard_KeyDown;

            // Apply theme styling
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            return dgv;
        }

        private void CreatePlusTab()
        {
            _plusTab = new TabPage("+");
            _plusTab.Tag = "PLUS_TAB"; // Marker to identify this special tab

            // Add a label to make the tab clickable and visible
            Label lblPlus = new Label
            {
                Text = "Click to add new bill",
                AutoSize = false,
                Size = new Size(691, 420),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Dock = DockStyle.Fill
            };
            _plusTab.Controls.Add(lblPlus);

            tabControlBills.TabPages.Add(_plusTab);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if "+" tab was selected
            if (tabControlBills.SelectedTab == _plusTab)
            {
                // Don't allow the "+" tab to actually be selected
                // Create a new tab instead
                var newTab = CreateNewBillTab();
                if (newTab != null)
                {
                    tabControlBills.SelectedTab = newTab;
                }
                else if (_previousTab != null)
                {
                    // If we hit the limit, go back to previous tab
                    tabControlBills.SelectedTab = _previousTab;
                }
                return;
            }

            // Get previous tab controls
            BillTabControls prevControls = null;
            if (_previousTab != null && _previousTab != _plusTab)
            {
                prevControls = GetTabControls(_previousTab);
            }

            // Hide suggestion list from previous tab
            if (prevControls != null)
            {
                prevControls.SuggestionList.Visible = false;

                // End any ongoing edit operation
                if (prevControls.CartGrid.IsCurrentCellInEditMode)
                {
                    prevControls.CartGrid.EndEdit();
                }
            }

            // Save previous tab's session state
            if (_currentSession != null && prevControls != null)
            {
                SavePreviousTabState(prevControls);
            }

            // Load new tab's session state
            LoadSessionState(tabControlBills.SelectedTab);

            // Update previous tab tracker
            _previousTab = tabControlBills.SelectedTab;
        }

        private void SavePreviousTabState(BillTabControls controls)
        {
            if (_currentSession == null || controls == null) return;

            // Save cart items
            _currentSession.CartItems.Clear();
            foreach (DataGridViewRow row in controls.CartGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    _currentSession.CartItems.Add(new CartItem(row));
                }
            }

            // Save financial values
            _currentSession.Subtotal = _subtotal;
            _currentSession.DiscountPercent = _discountPercent;
            _currentSession.SideDiscount = _sideDiscount;
            _currentSession.TaxPercent = _taxPercent;

            double.TryParse(lblTotalValue.Text, out double total);
            _currentSession.Total = total;

            double.TryParse(txtPayment.Text, out double payment);
            _currentSession.Payment = payment;

            double.TryParse(txtChange.Text, out double change);
            _currentSession.Change = change;

            // Save search text
            _currentSession.SearchText = controls.SearchBox.Text;

            // Mark dirty if has items
            _currentSession.MarkDirty();
        }

        private void TabControl_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if user clicked on the "+" tab header
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < tabControlBills.TabPages.Count; i++)
                {
                    Rectangle tabRect = tabControlBills.GetTabRect(i);
                    if (tabRect.Contains(e.Location))
                    {
                        TabPage clickedTab = tabControlBills.TabPages[i];

                        // If "+" tab was clicked
                        if (clickedTab == _plusTab)
                        {
                            // Prevent default selection behavior
                            var newTab = CreateNewBillTab();
                            if (newTab != null)
                            {
                                tabControlBills.SelectedTab = newTab;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            // Right-click to close tab (with confirmation)
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControlBills.TabPages.Count; i++)
                {
                    Rectangle tabRect = tabControlBills.GetTabRect(i);
                    if (tabRect.Contains(e.Location))
                    {
                        TabPage tab = tabControlBills.TabPages[i];

                        // Don't allow closing "+" tab
                        if (tab == _plusTab) return;

                        // Don't allow closing last bill tab
                        if (_tabManager.GetActiveTabCount() <= 1)
                        {
                            MessageBox.Show("Cannot close the last bill tab.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        CloseTab(tab);
                        break;
                    }
                }
            }
        }

        private void CloseTab(TabPage tab)
        {
            var controls = GetTabControls(tab);
            if (controls == null) return;

            // Check if tab has unsaved items
            if (controls.Session.IsDirty)
            {
                DialogResult result = MessageBox.Show(
                    $"Bill #{controls.Session.TabNumber} has unsaved items. Close anyway?",
                    "Confirm Close",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result != DialogResult.Yes)
                    return;
            }

            // Remove from manager
            _tabManager.RemoveTab(tab);

            // Dispose controls
            tab.Controls.Clear();

            // Remove tab
            tabControlBills.TabPages.Remove(tab);

            // Select another tab
            if (tabControlBills.TabPages.Count > 1)
                tabControlBills.SelectedIndex = 0;
        }

        private void SaveCurrentSessionState()
        {
            if (_currentSession == null) return;

            var controls = GetCurrentTabControls();
            if (controls == null) return;

            // Save cart items
            _currentSession.CartItems.Clear();
            foreach (DataGridViewRow row in controls.CartGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    _currentSession.CartItems.Add(new CartItem(row));
                }
            }

            // Save financial values
            _currentSession.Subtotal = _subtotal;
            _currentSession.DiscountPercent = _discountPercent;
            _currentSession.SideDiscount = _sideDiscount;
            _currentSession.TaxPercent = _taxPercent;

            double.TryParse(lblTotalValue.Text, out double total);
            _currentSession.Total = total;

            double.TryParse(txtPayment.Text, out double payment);
            _currentSession.Payment = payment;

            double.TryParse(txtChange.Text, out double change);
            _currentSession.Change = change;

            // Save search text
            _currentSession.SearchText = controls.SearchBox.Text;

            // Mark dirty if has items
            _currentSession.MarkDirty();
        }

        private void LoadSessionState(TabPage tab)
        {
            var controls = GetTabControls(tab);
            if (controls == null) return;

            _currentSession = controls.Session;

            // Clear current cart grid
            controls.CartGrid.Rows.Clear();

            // Restore cart items
            foreach (var item in _currentSession.CartItems)
            {
                item.AddToGrid(controls.CartGrid);
            }

            // Restore financial values
            _subtotal = _currentSession.Subtotal;
            _discountPercent = _currentSession.DiscountPercent;
            _sideDiscount = _currentSession.SideDiscount;
            _taxPercent = _currentSession.TaxPercent;

            // Update UI (right panel)
            lblSubtotalValue.Text = _currentSession.Subtotal.ToString("N2");
            lblDiscountValue.Text = _currentSession.SideDiscount.ToString("N2");
            txtTaxValue.Text = _currentSession.TaxPercent.ToString();
            lblTotalValue.Text = _currentSession.Total.ToString("N2");
            txtPayment.Text = _currentSession.Payment > 0 ? _currentSession.Payment.ToString() : "";
            txtChange.Text = _currentSession.Change.ToString("N2");

            // Restore search text
            controls.SearchBox.Text = _currentSession.SearchText;

            // Update button state
            CheckButtonsAccess();

            // Refresh image
            ShowSelectedProductImage();

            // Set focus to search box
            controls.SearchBox.Focus();
        }

        private BillTabControls GetCurrentTabControls()
        {
            if (tabControlBills.SelectedTab == null ||
                tabControlBills.SelectedTab == _plusTab)
                return null;

            return GetTabControls(tabControlBills.SelectedTab);
        }

        private BillTabControls GetTabControls(TabPage tab)
        {
            return tab.Tag as BillTabControls;
        }

        private BillTabControls GetControlsForSearchBox(TextBox searchBox)
        {
            // Find parent TabPage
            Control parent = searchBox.Parent;
            while (parent != null && !(parent is TabPage))
                parent = parent.Parent;

            if (parent is TabPage tab)
                return GetTabControls(tab);

            return null;
        }


        // ================= SEARCH =================
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox == null) return;

            // Get the tab controls for the search box's parent tab
            var controls = GetControlsForSearchBox(searchBox);
            if (controls == null) return;

            if (!blinkTimer.Enabled)
                blinkTimer.Start();

            string search = searchBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                controls.SuggestionList.Visible = false;
                return;
            }

            var products = _productService.GetAll()
    .Where(p =>
           p.Title.ToLower().Contains(search)
        || (p.Model != null && p.Model.ToLower().Contains(search))
        || (p.Category != null && p.Category.Title.ToLower().Contains(search))
        || (p.ProductCode != null && p.ProductCode.ToLower().Equals(search))
        || (p.CodeType == 1 && p.ProductCode.ToLower().Equals(search))   // 🔥 QR Code
        || (p.CodeType == 2 && p.ProductCode.ToLower().Equals(search))   // 🔥 Barcode
        || (p.Company != null && p.Company.Title.ToLower().Contains(search))
    )
    .ToList();


            controls.SuggestionList.DataSource = products;
            controls.SuggestionList.DisplayMember = "Title";
            controls.SuggestionList.ValueMember = "ProductId";
            controls.SuggestionList.Visible = products.Any();
            _suggestionIndex = -1;
        }
        private void lstSuggestion_Click(object sender, EventArgs e)

        {
            ListBox listBox = sender as ListBox;
            if (listBox == null || listBox.SelectedItem == null)
                return;

            var selectedProduct = (Product)listBox.SelectedItem;
            AddToCartData(selectedProduct);

            // Get controls to clear and hide
            Control parent = listBox.Parent;
            while (parent != null && !(parent is TabPage))
                parent = parent.Parent;

            if (parent is TabPage tab)
            {
                var controls = GetTabControls(tab);
                if (controls != null)
                {
                    controls.SuggestionList.Visible = false;
                    controls.SearchBox.Clear();
                }
            }

            ShowSelectedProductImage();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            // Start blinking when the TextBox gets focus
            blinkTimer.Start();
        }


        private void txtSearch_Leave(object sender, EventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox == null) return;

            blinkTimer.Stop();
            searchBox.BackColor = Color.White;
        }

        // ================= BARCODE SCANNER =================
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Process barcode scanner input (Symbol scanner sends keystrokes)
            _barcodeReader.ProcessKeyPress(sender, e);
        }

        private void BarcodeReader_BarcodeScanned(object sender, BarcodeScannedEventArgs e)
        {
            try
            {
                // Search for product by barcode (includes QR Code, Bar Code, and Manual Code)
                var product = _productService.GetAll()
                    .FirstOrDefault(p => p.ProductCode != null &&
                                       p.ProductCode.Equals(e.Barcode, StringComparison.OrdinalIgnoreCase) &&
                                       (p.CodeType == 1 || p.CodeType == 2 || p.CodeType == 3)); // QR, Barcode, or Manual Code

                if (product != null)
                {
                    // Add product to current active bill tab automatically
                    AddToCartData(product);

                    // Play success beep
                    SystemSounds.Beep.Play();

                    // Clear search box
                    var controls = GetCurrentTabControls();
                    if (controls != null)
                    {
                        controls.SearchBox.Clear();
                        controls.SuggestionList.Visible = false;
                    }

                    // Flash success feedback
                    FlashSuccessFeedback();
                }
                else
                {
                    // Product not found - play error sound and show message
                    SystemSounds.Hand.Play();
                    MessageBox.Show($"Product with code '{e.Barcode}' not found!",
                                  "Product Not Found",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);

                    // Clear search box
                    var controls = GetCurrentTabControls();
                    if (controls != null)
                    {
                        controls.SearchBox.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing barcode: {ex.Message}",
                              "Barcode Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void FlashSuccessFeedback()
        {
            // Flash the form background briefly to indicate successful scan
            var controls = GetCurrentTabControls();
            if (controls != null && controls.SearchBox != null)
            {
                var originalColor = controls.SearchBox.BackColor;
                controls.SearchBox.BackColor = Color.LightGreen;

                var flashTimer = new Timer { Interval = 200 };
                flashTimer.Tick += (s, e) =>
                {
                    controls.SearchBox.BackColor = originalColor;
                    flashTimer.Stop();
                    flashTimer.Dispose();
                };
                flashTimer.Start();
            }
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            txtSearch.BackColor = Color.White; // always white
        }



        private void AddToCartData(Product product)
        {
            var controls = GetCurrentTabControls();
            if (controls == null) return;

            // Check if product already exists in cart
            DataGridViewRow existingRow = null;
            foreach (DataGridViewRow row in controls.CartGrid.Rows)
            {
                if (!row.IsNewRow && row.Cells["ProductId"].Value != null)
                {
                    int existingProductId = Convert.ToInt32(row.Cells["ProductId"].Value);
                    if (existingProductId == product.ProductId)
                    {
                        existingRow = row;
                        break;
                    }
                }
            }

            int rowIndex;

            if (existingRow != null)
            {
                // Product already exists - increase quantity
                int currentQuantity = Convert.ToInt32(existingRow.Cells["Quantity"].Value);
                int newQuantity = currentQuantity + 1;
                existingRow.Cells["Quantity"].Value = newQuantity;

                // Recalculate total for this row
                double price = Convert.ToDouble(existingRow.Cells["SalePrice"].Value);
                double newTotal = newQuantity * price;
                existingRow.Cells["Total"].Value = newTotal;

                rowIndex = existingRow.Index;
            }
            else
            {
                // Product doesn't exist - add new row
                double total = Convert.ToDouble(product.SalePrice);

                rowIndex = controls.CartGrid.Rows.Add(
                    product.ProductId,
                    product.Title,
                    1,
                    product.SalePrice,
                    product.Discount,
                    total,
                    product.UrlImage
                );
            }

            controls.CartGrid.ClearSelection();
            controls.CartGrid.Rows[rowIndex].Selected = true;
            controls.CartGrid.CurrentCell = controls.CartGrid.Rows[rowIndex].Cells[1];

            UpdateTotals();
            CheckButtonsAccess();
            ShowSelectedProductImage();
        }


        private void CalculateChange()
        {
            double payment = 0;
            double total = 0;

            double.TryParse(txtPayment.Text, out payment);
            double.TryParse(lblTotalValue.Text, out total);

            double change = payment - total;

            if (change < 0)
                change = 0;

            txtChange.Text = change.ToString("N2");
        }
        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }



        private void UpdateTotals()
        {
            var controls = GetCurrentTabControls();
            if (controls == null) return;

            _subtotal = 0;
            double gridDiscount = 0;

            // 1️⃣ Subtotal & Row Discount collect
            foreach (DataGridViewRow row in controls.CartGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    double rowTotal = 0;
                    double rowDiscount = 0;

                    double.TryParse(row.Cells["Total"].Value?.ToString(), out rowTotal);
                    double.TryParse(row.Cells["Discount"].Value?.ToString(), out rowDiscount);

                    _subtotal += rowTotal;
                    gridDiscount = rowDiscount;

                }
            }

            // 2️⃣ Side Discount also read
            double sideDiscount = 0;
            double.TryParse(lblDiscountValue.Text, out sideDiscount);

            double totalDiscount = gridDiscount + sideDiscount;

            if (totalDiscount > _subtotal)
                totalDiscount = _subtotal;

            // 3️⃣ Tax
            double tax = 0;
            double.TryParse(txtTaxValue.Text, out tax);

            double totalAfterDiscount = _subtotal - totalDiscount;
            double taxAmount = totalAfterDiscount * (tax / 100.0);
            double finalTotal = totalAfterDiscount + taxAmount;

            // 4️⃣ UI update
            lblSubtotalValue.Text = _subtotal.ToString("N2");
            lblDiscountValue.Text = totalDiscount.ToString("N2");
            lblTotalValue.Text = finalTotal.ToString("N2");

            // 5️⃣ Change
            CalculateChange();
        }



        private void UpdateRowTotal(DataGridView dgv, int rowIndex)
        {
            DataGridViewRow row = dgv.Rows[rowIndex];

            double qty = Convert.ToDouble(row.Cells["Quantity"].Value);
            double price = Convert.ToDouble(row.Cells["SalePrice"].Value);

            double discount = 0;
            if (row.Cells["Discount"].Value != null)
                double.TryParse(row.Cells["Discount"].Value.ToString(), out discount);

            double total = (qty * price) - discount;

            if (total < 0)
                total = 0;

            row.Cells["Total"].Value = total.ToString("N2");

            UpdateTotals();
        }


        private void dgvAddToCard_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            if (e.ColumnIndex == dgv.Columns["Quantity"].Index ||
                e.ColumnIndex == dgv.Columns["SalePrice"].Index ||
                e.ColumnIndex == dgv.Columns["Discount"].Index)   // ✅ NEW
            {

                UpdateRowTotal(dgv, e.RowIndex);
            }
        }

        private void dgvAddToCard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            if (e.RowIndex >= 0 &&
                e.ColumnIndex == dgv.Columns["SalePrice"].Index)
            {
                dgv.BeginEdit(true);
            }
        }

        private void dgvAddToCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0)
                return;

            if (dgv.Columns[e.ColumnIndex].Name == "Delete")
            {
                DataGridViewRow row = dgv.Rows[e.RowIndex];
                if (row.IsNewRow)
                    return;
                dgv.Rows.RemoveAt(e.RowIndex);
                UpdateTotals();
                CheckButtonsAccess();   // <-- button disable enable here
            }

        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (_selectedBox == null)
            {
                MessageBox.Show("Please select a field first (Tax, Discount, Payment, Total)");
                return;
            }

            Button btn = sender as Button;

            if (btn != null)
            {
                _selectedBox.Text += btn.Text;
            }
            UpdateTotals();

        }
        private void txtTaxValue_Click(object sender, EventArgs e)
        {
            _selectedBox = txtTaxValue;
        }

        private void lblDiscountValue_Click(object sender, EventArgs e)
       {
            _selectedBox = lblDiscountValue as TextBox;
        }

        private void txtPayment_Click(object sender, EventArgs e)
        {
            _selectedBox = txtPayment;
        }

        private void lblTotalValue_Click(object sender, EventArgs e)
        {
            _selectedBox = lblTotalValue as TextBox;
        }
       
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_selectedBox != null)
            {
                _selectedBox.Clear();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_selectedBox != null && _selectedBox.Text.Length > 0)
            {
                _selectedBox.Text = _selectedBox.Text.Substring(0, _selectedBox.Text.Length - 1);
            }
        }




        // ================= BILL CREATION =================
        private void BillConfirm_Click(object sender, EventArgs e)
        {
            var controls = GetCurrentTabControls();
            if (controls == null) return;

            int realRows = controls.CartGrid.Rows
                 .Cast<DataGridViewRow>()
                 .Count(r => !r.IsNewRow);

            if (realRows == 0)
            {
                MessageBox.Show("No items in cart!");
                return;
            }

            // ------- STOCK VALIDATION -------
            bool stockValid = ValidateStockAvailability(controls.CartGrid);
            if (!stockValid)
            {
                MessageBox.Show("Some items are out of stock or have insufficient quantity. Please review your cart.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ------- 1️⃣ Save Customer -------
            var customer = new CustomerInvoice()
            {
                CustomerName = "Customer Name",
                CustomerPhone = "Phone",
                CustomerAddress = "Address"
            };

            customer = _customerService.Add(customer);

            // ------- 2️⃣ Save Bill -------
            decimal discount = 0;
            decimal tax = 0;
            decimal total = 0;
            decimal subtotal = 0;
            decimal grand = 0;

            decimal.TryParse(lblDiscountValue.Text, out discount);
            decimal.TryParse(txtTaxValue.Text, out tax);
            decimal.TryParse(lblSubtotalValue.Text, out subtotal);
            decimal.TryParse(lblTotalValue.Text, out total);
            decimal.TryParse(lblTotalValue.Text, out grand);

            var bill = new Bill()
            {
                UserId = SessionManager.UserId,
                Role = SessionManager.Role,
                CustomerInvoiceId = customer.Id,
                BillDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                OwnDate = DateTime.Now,
                Discount = discount,
                itemPrice = subtotal,
                TotalAmount = total,
                Tax = tax,
                GrandTotal = grand,
                PaymentMethod = "Cash"
            };


            bill = _billService.Add(bill);

            // ------- 3️⃣ Save Bill Products -------
            List<BillProduct> billProducts = new List<BillProduct>();

            foreach (DataGridViewRow row in controls.CartGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    billProducts.Add(new BillProduct
                    {
                        BillId = bill.BillId,
                        ProductId = Convert.ToInt32(row.Cells["ProductId"].Value),
                        Title = row.Cells["Title"].Value.ToString(),
                        Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                        ItemPrice = Convert.ToDecimal(row.Cells["SalePrice"].Value),
                        TotalPrice = Convert.ToDecimal(row.Cells["Total"].Value)
                    });
                }
            }

            _billProductService.AddRange(billProducts);

            // ------- 4️⃣ Load Full Bill for Printing -------
            var BillData = _billService.GetBillWithDetails(bill.BillId);
            PrintForm printForm = new PrintForm(BillData,this);
            printForm.ShowDialog();

            // ------- 5️⃣ Clear Current Tab After Successful Bill -------
            ClearCurrentTab();
        }

        private bool ValidateStockAvailability(DataGridView dgv)
        {
            bool allValid = true;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                    int requestedQty = Convert.ToInt32(row.Cells["Quantity"].Value);

                    var product = _productService.GetById(productId);
                    if (product == null || product.Quantity < requestedQty)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        allValid = false;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }

            return allValid;
        }

        private void ClearCurrentTab()
        {
            var controls = GetCurrentTabControls();
            if (controls == null) return;

            controls.CartGrid.Rows.Clear();
            controls.SearchBox.Clear();
            controls.Session.CartItems.Clear();
            controls.Session.IsDirty = false;

            // Reset totals
            _subtotal = 0;
            _discountPercent = 0;
            _sideDiscount = 0;
            _taxPercent = 0;

            lblSubtotalValue.Text = "0.00";
            lblDiscountValue.Text = "0.00";
            txtTaxValue.Text = "0";
            lblTotalValue.Text = "0.00";
            txtPayment.Clear();
            txtChange.Text = "0.00";

            CheckButtonsAccess();
        }
        // ================= NAVIGATION =================
        private void LoginPanelReports(object sender, EventArgs e)
        {
            var report = new ReportForm();
            report.Show();
            
        }

        private void LoginPanelSales(object sender, EventArgs e)
        {
            var sales = new SalesForm();
            sales.Show();
            
        }

        private void LoginPanelUsers(object sender, EventArgs e)
        {
            var user = new UserForm();
            user.Show();
            
        }

        private void LoginPanelProduct(object sender, EventArgs e)
        {
            var product = new ProductForm();
            product.Show();
           
        }

        private void LoginPanelbtnHome(object sender, EventArgs e)
        {
            var home = new Home();
            home.Show();
           
        }

        // ================= TAX / DISCOUNT EVENTS =================
        private void txtTaxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(txtTaxValue.Text, out double taxPercent))
                    _taxPercent = taxPercent;
                else
                    _taxPercent = 0;

                UpdateTotals();
                e.SuppressKeyPress = true;
            }
        }

        private void lblDiscountValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(lblDiscountValue.Text, out double discountPercent))
                    _discountPercent = discountPercent;
                else
                    _discountPercent = 0;

                UpdateTotals();
                e.SuppressKeyPress = true;
            }
        }
        public void ClearCart()
        {
            dgvAddToCard.Rows.Clear();
            lblSubtotalValue.Text = "0";
            lblDiscountValue.Text = "0";
            txtTaxValue.Text = "0";
            lblTotalValue.Text = "0";
            CheckButtonsAccess();
        }
        private void CheckButtonsAccess()
        {
            var controls = GetCurrentTabControls();
            if (controls == null)
            {
                btnConfirm.Enabled = false;
                return;
            }

            bool hasRows = controls.CartGrid.Rows
                            .Cast<DataGridViewRow>()
                            .Any(r => !r.IsNewRow);

            btnConfirm.Enabled = hasRows;

        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox == null) return;

            var controls = GetControlsForSearchBox(searchBox);
            if (controls == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                // Handle Enter key to select item from suggestion list
                if (controls.SuggestionList.Visible && controls.SuggestionList.Items.Count > 0)
                {
                    // If no item selected yet, select the first one
                    if (_suggestionIndex < 0 || _suggestionIndex >= controls.SuggestionList.Items.Count)
                    {
                        _suggestionIndex = 0;
                        controls.SuggestionList.SelectedIndex = 0;
                    }

                    var selectedProduct = (Product)controls.SuggestionList.SelectedItem;
                    if (selectedProduct != null)
                    {
                        AddToCartData(selectedProduct);
                        controls.SuggestionList.Visible = false;
                        searchBox.Clear();
                        _suggestionIndex = -1;
                    }
                }
                e.SuppressKeyPress = true;
                return;
            }

            if (!controls.SuggestionList.Visible || controls.SuggestionList.Items.Count == 0)
                return;

            if (e.KeyCode == Keys.Down)
            {
                _suggestionIndex++;
                if (_suggestionIndex >= controls.SuggestionList.Items.Count)
                    _suggestionIndex = 0;
                controls.SuggestionList.SelectedIndex = _suggestionIndex;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                _suggestionIndex--;
                if(_suggestionIndex< 0 )
                    _suggestionIndex= controls.SuggestionList.Items.Count - 1;
                controls.SuggestionList.SelectedIndex = _suggestionIndex;
                e.SuppressKeyPress= true;
            }

        }

        private void dgvAddToCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPayment.Focus();
                e.SuppressKeyPress = true;
            }
        }
        private void txtPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                btnConfirm.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            this.Close();


        }
        private void dgvAddToCard_SelectionChanged(object sender, EventArgs e)
        {
            ShowSelectedProductImage();
        }
        private void ShowSelectedProductImage()
        {
            try
            {
                var controls = GetCurrentTabControls();
                if (controls == null || controls.CartGrid.CurrentRow == null) return;

                var cellValue = controls.CartGrid.CurrentRow.Cells["UrlImage"].Value;
                if (cellValue == null) return;

                string imagePath = cellValue.ToString();
                
                var img = _fileServices.GetFileByName(imagePath);
                
                if (img != null)
                {
                    if (picProduct.Image != null)
                    {
                        picProduct.Image.Dispose();
                        picProduct.Image = null;
                    }
                        picProduct.Image = img;
                }
                else
                {
                    picProduct.Image = null;
                }
            }
            catch
            {
                picProduct.Image = null;
            }
        }
        private void ApplyRoleAccess()
        {
            // Default: sab hide
            btnUsers.Visible = false;
            btnProducts.Visible = false;
            btnSales.Visible = false; 
            btnReports.Visible = false;
            

            btnHome.Visible = true; 

            // ADMIN
            if (SessionManager.Role == "Administrator")
            {
                btnUsers.Visible = true;
                btnProducts.Visible = true;
                btnSales.Visible = true;
                btnReports.Visible = true;
                btnSetting.Visible = true;
            }
            // NORMAL USER
            else 
            {
                btnUsers.Visible = false;
                btnProducts.Visible = false;
                btnSales.Visible = false;
                btnReports.Visible = false;
                btnSetting.Visible =false;
            }
        }
        public void LoadHeaderName()
        {
            var setting = _settingService.GetByKey("StoreName");

            if (setting != null)
            {
                lblMainHeaderName.Text = setting;

            }
            else 
            {
                lblMainHeaderName.Text = "";
            }
        }

        private void btnCloudBackup_Click(object sender, EventArgs e)
        {
            FrmGoogleDriveBackup form = new FrmGoogleDriveBackup();
            form.Show();
        }
    }
}
