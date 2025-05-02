using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Reports;
using RoboostTask.Features.Stocks.Queries;

namespace RoboostTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockReport()
        {
            var result = await _mediator.Send(new GetLowStockReportQuery());
            return Ok(result);
        }
    }
}
