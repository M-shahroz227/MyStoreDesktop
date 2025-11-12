using MyStoreDesktop.Models;
using MyStoreDesktop.Services.ProductService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private readonly ProductService _productService = new ProductService();
        private readonly Category _category = new Category();

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                lstSuggestion.Visible =false;
                return;
            }
            var products = _productService.GetAll().Where(p => p.Title.ToLower().Contains(search) ||
                                                         (p.Model!=null && p.Model.ToLower().Contains(search)) ||
                                                         (p.Category!=null && p.Category.Title.ToLower().Contains(search)) ||
                                                         (p.ProductCode != null && p.ProductCode.ToLower().Equals(search)) ||
                                                         (p.Company != null && p.Company.Title.ToLower().Contains(search))).Select(p=> p.Title)
                                                         .ToList();
            lstSuggestion.DataSource = products;
            lstSuggestion.DisplayMember = "Title"; 
            lstSuggestion.ValueMember = "ProductId";
            lstSuggestion.Visible= products.Any();
            

        }
        private void lstSuggestion_Click(object sender, EventArgs e)
        {
            if (lstSuggestion.SelectedItem == null)
                return;

            var selectedProduct = (Product)lstSuggestion.SelectedItem;

            AddToCartData(selectedProduct);

            lstSuggestion.Visible = false;
            txtSearch.Clear();
        }

        private void AddToCartData(Product product)
        {
            
            foreach (DataGridViewRow row in dgvAddToCard.Rows)
            {
                if (Convert.ToInt32(row.Cells["ProductId"].Value) == product.ProductId)
                {
                    int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    row.Cells["Quantity"].Value = currentQty + 1;
                    row.Cells["Total"].Value = (currentQty + 1) * product.SalePrice;
                    return;
                }
            }

          
            dgvAddToCard.Rows.Add(product.ProductId, product.Title,product.Quantity, product.SalePrice, product.Total);
        }

        private void LoginPanelbtnHome(object sender, EventArgs e)
        {
            var home = new Home();
            home.Show();
        }
        private void LoginPanelProduct(object sender, EventArgs e)
        {
            var product = new ProductForm();
            product.Show();
        }
        private void LoginPanelUsers(Object sender, EventArgs e)
        {
            var User = new UserForm();
            User.Show();
        }
        private void LoginPanelSales(Object sender, EventArgs e)
        { 
            var Sales = new SalesForm();
            Sales.Show();
        }
        private void LoginPanelReports(Object sender, EventArgs e)
        {
            var Report = new ReportForm();
            Report.Show();
        }

        
    }
}
