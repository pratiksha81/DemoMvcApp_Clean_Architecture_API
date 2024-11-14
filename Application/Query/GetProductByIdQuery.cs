using DemoMvcApp.Models;
using MediatR;

namespace DemoMvcApp.Query
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

}
