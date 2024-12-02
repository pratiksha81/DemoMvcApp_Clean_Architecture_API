using DemoMvcApp.Models;

namespace DemoMvcApp.Infrastructure
{
      //IEnumerable<Product> GetAllProducts();
        //Product GetProductById(int productId);
        //string AddProduct(Product product);
        //string UpdateProduct(int productId, Product product);
        //string DeleteProduct(int productId);

        public interface IProductRepository
        {
            IEnumerable<Product> GetAllProducts();
            Product GetProductById(int productId);
            string AddProduct(Product product);
            string UpdateProduct(int productId, Product product);
            string DeleteProduct(int productId);
        }

    }



