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
                                                         (p.ProductCode != null && p.ProductCode.ToLower().Contains(search))).Select(p=> p.Title)
                                                         .ToList();
            lstSuggestion.DataSource = products;
            lstSuggestion.Visible= products.Any();
            

        }
        private void LoginPanelProduct(object sender, EventArgs e)
        {
            var product = new ProductForm();
        }
    }
}
