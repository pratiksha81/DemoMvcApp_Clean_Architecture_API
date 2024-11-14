using DemoMvcApp.Models;

namespace DemoMvcApp.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product value);
        void UpdateProduct(int id, Product value);
        void DeleteProduct(int id);
    }
}

