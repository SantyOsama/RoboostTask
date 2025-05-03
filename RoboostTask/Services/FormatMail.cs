using RoboostTask.DTOs.Reports;
using RoboostTask.Models;

namespace RoboostTask.Services
{
    public class FormatEmail
    {
        public static string CreateLowStockEmail(List<Product> lowStockItems)
        {
            var emailBody = $@"<!DOCTYPE html
        PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
        <html>
        <head>
            <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" maximum-scale=""1"">
            <link href=""https://fonts.googleapis.com/css?family=Raleway:300,400,700,900"" rel=""stylesheet"">
            <style>
                body {{
                    font-family: 'Raleway', Helvetica, Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    color: #333;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #f9f9f9;
                    border-radius: 4px;
                    box-shadow: 0 2px 8px rgba(0,0,0,0.05);
                }}
                .header {{
                    text-align: center;
                    padding: 20px 0;
                }}
                .logo {{
                    max-width: 150px;
                }}
                .title {{
                    font-size: 24px;
                    color: #d9534f;
                    margin: 20px 0;
                }}
                .subtitle {{
                    font-size: 16px;
                    margin-bottom: 30px;
                }}
                .item-table {{
                    width: 100%;
                    border-collapse: collapse;
                    margin-bottom: 30px;
                }}
                .item-table th {{
                    background-color: #d9534f;
                    color: white;
                    padding: 10px;
                    text-align: left;
                }}
                .item-table td {{
                    padding: 10px;
                    border-bottom: 1px solid #ddd;
                }}
                .item-table tr:nth-child(even) {{
                    background-color: #f2f2f2;
                }}
                .highlight {{
                    color: #d9534f;
                    font-weight: bold;
                }}
                .footer {{
                    margin-top: 30px;
                    text-align: center;
                    font-size: 12px;
                    color: #777;
                }}
            </style>
        </head>
        <body>
            <div class=""container"">
                <div class=""header"">
                    <h1 class=""title"">Low Stock Alert</h1>
                    <p class=""subtitle"">The following items are below their minimum stock thresholds and need attention:</p>
                </div>

                <table class=""item-table"">
                    <thead>
                        <tr>
                            <th>Item Name</th>
                            <th>Current Stock</th>
                            <th>Threshold</th>
                        </tr>
                    </thead>
                    <tbody>";

            foreach (var item in lowStockItems)
            {
                emailBody += $@"
                        <tr>
                            <td><strong>{item.Name}</strong><br><small>{item.Description}</small></td>
                            <td class=""highlight"">{item.Quantity}</td>
                            <td>{item.LowStockThreshold}</td>
                        </tr>";
            }

            emailBody += $@"
                    </tbody>
                </table>

                <div class=""summary"">
                    <p><strong>Total low stock items:</strong> {lowStockItems.Count}</p>
                </div>

                <div class=""footer"">
                    <p>This is an automated notification. Please take appropriate action to replenish stock.</p>
                    <p>&copy; {DateTime.Now.Year} Your Company Name. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>";

            return emailBody;
        }

    }
}
