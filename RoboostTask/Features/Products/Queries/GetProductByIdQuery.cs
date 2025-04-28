using MediatR;
using RoboostTask.DTOs;
using RoboostTask.GeneralResponse;
namespace RoboostTask.Features.Products.Queries
{
    public class GetProductByIdQuery:IRequest<Response<ProductDTO>>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
