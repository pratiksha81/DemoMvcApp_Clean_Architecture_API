using DemoMvcApp.Handler;
using DemoMvcApp.Models;
using DemoMvcApp.Infrastructure;

namespace DemoMvcApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IErrorHandlingService<string> _errorHandlingService;

        public ProductService(IProductRepository productRepository, IErrorHandlingService<string> errorHandlingService)
        {
            _productRepository = productRepository;
            _errorHandlingService = errorHandlingService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);  // Log the error
                throw;  // Rethrow the exception to be handled by the controller if necessary
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                return _productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);  // Log the error
                throw;  // Rethrow the exception
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                _productRepository.AddProduct(product);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);  // Log the error
                throw;  // Rethrow the exception
            }
        }

        public void UpdateProduct(int id, Product product)
        {
            try
            {
                _productRepository.UpdateProduct(id, product);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);  // Log the error
                throw;  // Rethrow the exception
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);  // Log the error
                throw;  // Rethrow the exception
            }
        }
    }
}
