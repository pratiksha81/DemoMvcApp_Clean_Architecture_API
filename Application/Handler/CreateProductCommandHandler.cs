
using DemoMvcApp.command;
using DemoMvcApp.Models;
using DemoMvcApp.Infrastructure;

using MediatR;

namespace DemoMvcApp.Handler
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                ProductImage = command.ProductImage
               /* ImagePath = command.ImagePath*/


            };

            _productRepository.AddProduct(product);  // Assuming AddProduct is a synchronous method
            return product;
        }
    }
}
