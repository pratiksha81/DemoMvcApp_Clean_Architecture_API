using DemoMvcApp.Data;
using DemoMvcApp.Handler;
using DemoMvcApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoMvcApp.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IErrorHandlingService<string> _errorHandlingService;

        public ProductRepository(ApplicationDbContext context, IErrorHandlingService<string> errorHandlingService)
        {
            _context = context;
            _errorHandlingService = errorHandlingService;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                return _context.Products.Find(id);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                Save();
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }

        public void UpdateProduct(int id, Product product)
        {
            try
            {
                var existingProduct = _context.Products.Find(id);

                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    Save();
                }
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    Save();
                }
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }

        private void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }
    }
}
