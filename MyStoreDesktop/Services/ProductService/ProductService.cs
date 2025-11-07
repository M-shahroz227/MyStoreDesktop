using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyStoreDesktop.Data;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DatabaseHelper _context;

        public ProductService()
        {
            _context = new DatabaseHelper();
        }

        public IEnumerable<Product> GetAll() => _context.Products.Include(p => p.Category).Include(p => p.Company).ToList();

        public Product GetById(int id) => _context.Products.Include(p => p.Category).Include(p => p.Company).FirstOrDefault(p => p.ProductId == id);

        // 🔹 Return type change kiya: void → Product
        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); // yahan ProductId generate hota hai
            return product; // ✅ ab ProductId return karega
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetCategories() => _context.Categories.OrderBy(c => c.Title).ToList();

        public Category GetCategoryById(int id) => _context.Categories.Find(id);

        public Category AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return;
            }

            if (category.Products.Any())
            {
                throw new System.InvalidOperationException("Cannot delete a category that has associated products.");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Company> GetCompanies() => _context.Companies.OrderBy(c => c.Title).ToList();

        public Company GetCompanyById(int id) => _context.Companies.Find(id);

        public Company AddCompany(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
            return company;
        }

        public void UpdateCompany(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCompany(int id)
        {
            var company = _context.Companies.Include(c => c.Products).FirstOrDefault(c => c.CompanyId == id);
            if (company == null)
            {
                return;
            }

            if (company.Products.Any())
            {
                throw new System.InvalidOperationException("Cannot delete a company that has associated products.");
            }

            _context.Companies.Remove(company);
            _context.SaveChanges();
        }
    }
}
