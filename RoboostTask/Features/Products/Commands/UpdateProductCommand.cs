using MediatR;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Features.Products.Commands
{
    public record UpdateProductCommand(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int Quantity,
        int LowStockThreshold
    ) : IRequest<Response<string>>;
}
