using DemoMvcApp.DTOs;
using DemoMvcApp.Models;

namespace DemoMvcApp.Mapper
{
    public class Mapper : IMapper
    {
        public ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
               /* ProductImage = product.ProductImage,*/
                /*ImagePath = product.ImagePath*/
            };
        }

        public Product MapToEntity(ProductDto productDto)
        {
            return new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price
              /*  ProductImage = productDto.ProductImage,
                ImagePath = productDto.ImagePath*/
            };
        }
    }

}
