using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
namespace RoboostTask.Features.Products.Queries
{
    public class GetProductByIdQuery:IRequest<Response<GetProductResponse>>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
