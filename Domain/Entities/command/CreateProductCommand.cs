using DemoMvcApp.Models;
using MediatR;

namespace DemoMvcApp.command
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
      /*  public string ImagePath { get; set; }*/

    }

}
