using System.Collections.Generic;
using MyStoreDesktop.Models;

namespace MyStoreDesktop.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);

        // 🔹 Return type change kiya (void → Product)
        Product Add(Product product);

        void Update(Product product);
        void Delete(int id);

        // Category operations
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int id);
        Category AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

        // Company operations
        IEnumerable<Company> GetCompanies();
        Company GetCompanyById(int id);
        Company AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(int id);
    }
}
