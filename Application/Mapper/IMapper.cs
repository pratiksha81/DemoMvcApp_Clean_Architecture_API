using DemoMvcApp.DTOs;
using DemoMvcApp.Models;

namespace DemoMvcApp.Mapper
{
    public interface IMapper
    {
        ProductDto MapToDto(Product product);
        Product MapToEntity(ProductDto productDto);
    }

}
