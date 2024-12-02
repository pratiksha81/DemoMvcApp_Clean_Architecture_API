using Dapper;
using DemoMvcApp.Data;
using DemoMvcApp.Handler;
using DemoMvcApp.Models;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data; // Required for transactions

namespace DemoMvcApp.Infrastructure
{
    /*public class ProductRepository : IProductRepository
    {
        private readonly DatabaseConfig DBConfig;
         private readonly IErrorHandlingService<string> _errorHandlingService;

        public ProductRepository(DatabaseConfig context, IErrorHandlingService<string> errorHandlingService)
        {
            DBConfig = context;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                using var connection = DBConfig.CreateConnection(); // Ensure connection is properly initialized
                return await connection.QueryAsync<Product>(
                    "spGetAllProducts",
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError($"Error fetching products: {ex.Message}");
                throw;
            }
        }


        public Product GetProductById(int id)
        {
            try
            {
                return .Products.Find(id);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                throw;
            }
        }

        public void AddProduct(Product product)
        {
            using var transaction = DBConfig.Database.BeginTransaction();
            try
            {
                DBConfig.Products.Add(product);
                DBConfig.SaveChanges();
                 transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _errorHandlingService.SetError($"Error adding product: {ex.Message}");
                throw;
            }
        }

        public void UpdateProduct(int id, Product product)
        {
            using var transaction = DBConfig.Database.BeginTransaction();
            try
            {
                var existingProduct = DBConfig.Products.Find(id);
                if (existingProduct == null)
                {
                    throw new Exception($"Product with ID {id} not found.");
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;

                DBConfig.Products.Update(existingProduct);
                DBConfig.SaveChanges();
                 transaction.Commit();
            }
            catch (Exception ex)
            {
                 transaction.Rollback();
                _errorHandlingService.SetError($"Error updating product: {ex.Message}");
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            using var transaction = DBConfig.Database.BeginTransaction();
            try
            {
                var product = DBConfig.Products.Find(id);
                if (product == null)
                {
                    throw new Exception($"Product with ID {id} not found.");
                }

                DBConfig.Products.Remove(product);
                DBConfig.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _errorHandlingService.SetError($"Error deleting product: {ex.Message}");
                throw;
            }
        }
       
    }*/














    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");

                return connection.Query<Product>(
                    "SP_Product",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Product GetProductById(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "SE");
                parameters.Add("@ProductID", productId);

                return connection.QueryFirstOrDefault<Product>(
                    "SP_Product",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public string AddProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "I");
                parameters.Add("@ProductID", product.Id);
                parameters.Add("@Name", product.Name);
                parameters.Add("@Price", product.Price);
                parameters.Add("@ImageUrl", product.ProductImage);

                var result = connection.QueryFirstOrDefault<dynamic>(
                    "SP_Product",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed.";
            }
        }

        public string UpdateProduct(int productId, Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "U");
                parameters.Add("@ProductID", productId);
                parameters.Add("@Name", product.Name);
                parameters.Add("@Price", product.Price);
                parameters.Add("@ImageUrl", product.ProductImage);

                var result = connection.QueryFirstOrDefault<dynamic>(
                    "SP_Product",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed.";
            }
        }

        public string DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@flag", "D");
                parameters.Add("@ProductID", productId);

                var result = connection.QueryFirstOrDefault<dynamic>(
                    "SP_Product",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result?.Msg ?? "Operation failed.";
            }
        }
    }

}
