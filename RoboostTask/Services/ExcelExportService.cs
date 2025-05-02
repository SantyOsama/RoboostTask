using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Data;
using System.Drawing;

namespace RoboostTask.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportToExcel<T>(List<T> data, string sheetName = "Sheet1")
        {
            // Initialize the package with the license context
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // Load data with headers
            worksheet.Cells["A1"].LoadFromCollection(data, true);

            // Style the entire table
            worksheet.Cells[worksheet.Dimension.Address].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Style header row
            using (var header = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                header.Style.Font.Bold = true;
                header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                header.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                header.Style.Font.Color.SetColor(Color.White);
            }

            // Add conditional formatting for deficit amounts
            var properties = typeof(T).GetProperties();
            var deficitCol = Array.FindIndex(properties, p => p.Name == "DeficitAmount") + 1;

            if (deficitCol > 0)
            {
                var range = worksheet.Cells[2, deficitCol, worksheet.Dimension.End.Row, deficitCol];

                // Format positive deficits (bad)
                var cfPositive = range.ConditionalFormatting.AddGreaterThan();
                cfPositive.Formula = "0";
                cfPositive.Style.Fill.BackgroundColor.Color = Color.Red;
                cfPositive.Style.Font.Color.Color = Color.White;

                // Format negative/zero deficits (good)
                var cfNegative = range.ConditionalFormatting.AddLessThanOrEqual();
                cfNegative.Formula = "0";
                cfNegative.Style.Fill.BackgroundColor.Color = Color.Green;
            }

            // Freeze header row
            worksheet.View.FreezePanes(2, 1);

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}
