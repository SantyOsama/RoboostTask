using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Reports;
using RoboostTask.Features.Reports.Orchestrators;
using RoboostTask.Services;

namespace RoboostTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> GetLowStockReport([FromQuery] Guid? categoryId)
        {
            var result = await _mediator.Send(new GetLowStockReportOrchestrator(categoryId));
            return Ok(result);
        }
        [HttpGet("low-stock/excel")]
        public async Task<IActionResult> DownloadLowStockReportExcel([FromQuery] Guid? categoryId)
        {

        var report = await _mediator.Send(new GetLowStockReportOrchestrator(categoryId));

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
        [HttpGet("transaction-history")]
        public async Task<ActionResult<List<TransactionHistoryDTO>>> GetTransactionHistoryReport(
        [FromQuery] TransactionHistoryFilterDTO filter)
        {
            var query = new GetTransactionHistoryReportOrchestrator(filter);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("transaction-history/excel")]
        public async Task<IActionResult> DownloadTransactionHistoryReportExcel([FromQuery] TransactionHistoryFilterDTO filter)
        {
            var report = await _mediator.Send(new GetTransactionHistoryReportOrchestrator(filter));

            var excelData = report.Select(r => new
            {
                Product_Name = r.ProductName,
                Quantity = r.Quantity,
                Transaction_Type = r.TransactionType.ToString(),
                Date = r.Date,
                User = r.UserName,
                Source_Warehouse = r.SourceWarehouse,
                Destination_Warehouse = r.DestinationWarehouse

            }).ToList();

            var excelBytes = _excelService.ExportToExcel(excelData, "Transaction_History_Report");

            return File(excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Transaction_History_Report{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        }
    }
}
