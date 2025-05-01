using MediatR;
using RoboostTask.DTOs;

namespace RoboostTask.Features.Transaction.Queries
{
    public class GetInventoryQuery:IRequest<List<InventoryDTO>>
    {
    }
}
