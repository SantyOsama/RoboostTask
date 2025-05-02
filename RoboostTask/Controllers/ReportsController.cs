using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Reports;
using RoboostTask.Features.Stocks.Queries;
using RoboostTask.Services;

namespace RoboostTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IExcelExportService _excelService;

        public ReportsController(IMediator mediator, IExcelExportService excelService)
        {
            _mediator = mediator;
            _excelService = excelService;

        }
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockReport()
        {
            var result = await _mediator.Send(new GetLowStockReportQuery());
            return Ok(result);
        }
        [HttpGet("low-stock/excel")]
        public async Task<IActionResult> DownloadLowStockReportExcel()
        {

        var report = await _mediator.Send(new GetLowStockReportQuery());

        var excelData = report.Select(r => new
        {
            Product_Name = r.Name,
            Current_Stock = r.Quantity,
            Low_Stock_Threshold = r.LowStockThreshold,
            Deficit_Amount = r.DeficitAmount,
            Warehouse_Count = r.TotalWarehouses,
            Total_Stock = r.TotalActiveStock,
            Stock_Details = string.Join(" | ",
                r.Stocks.Select(s => $"{s.QuantityInStock}"))
        }).ToList();

        var excelBytes = _excelService.ExportToExcel(excelData, "Low_Stock_Report");

        return File(excelBytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Low_Stock_Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        }
    }
}
