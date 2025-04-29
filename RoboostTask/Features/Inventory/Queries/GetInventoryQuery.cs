using MediatR;
using RoboostTask.DTOs;

namespace RoboostTask.Features.Inventory.Queries
{
    public class GetInventoryQuery:IRequest<List<InventoryDTO>>
    {
    }
}
