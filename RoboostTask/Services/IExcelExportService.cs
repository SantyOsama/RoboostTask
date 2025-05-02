namespace RoboostTask.Services
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel<T>(List<T> data, string sheetName = "Sheet1");
    }
}
