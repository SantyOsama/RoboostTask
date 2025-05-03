using MediatR;
using RoboostTask.DTOs.Reports;
using RoboostTask.Models;

namespace RoboostTask.Features.Reports.Queries
{
    public record GetLowStockProductsQuery() : IRequest<List<Product>>;

}
