using DemoMvcApp.Models;

namespace DemoMvcApp.Services
{
    public interface IProductService 
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product value);
        void UpdateProduct(int id, Product value);
        void DeleteProduct(int id);
    }
}

