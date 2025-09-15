using API_Printing.Reports;
using API_Printing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;


namespace API_Printing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBill01Controller : ControllerBase
    {
        [HttpGet("GetEBill")]
        public async Task<IActionResult> GetEBill(
            [FromQuery] string? block,
            [FromQuery] string? Category,
            [FromQuery] string? month,
            [FromQuery] string? year,
            [FromQuery] string? Project,
            [FromServices] BillingService billingService)
        {
            try
            {
                var report = new EBill01();

                if (report == null)
                {
                    return StatusCode(500, "Failed to initialize the ElectricityBill report.");
                }

                // Helper function to safely set parameters
                void SetParameter(string paramName, object? value)
                {
                    if (report.Parameters[paramName] != null)
                    {
                        report.Parameters[paramName].Value = value;
                        report.Parameters[paramName].Visible = false;
                    }
                }

                SetParameter("Category", Category);
                SetParameter("Block", block);
                SetParameter("BillingMonth", month);
                SetParameter("BillingYear", year);
                SetParameter("Project", Project);

                // 🔹 Increase SQL command timeout (default is 30s)
                if (report.DataSource is SqlDataSource sqlDataSource)
                {
                    sqlDataSource.ConnectionOptions.CommandTimeout = 120; // 120 seconds
                }

                using var stream = new MemoryStream();
                report.ExportToPdf(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream.ToArray(), "application/pdf", "ElectricityBill.pdf");
            }
            catch (Exception ex)
            {
                // Return full error message for debugging
                return StatusCode(500, $"Error generating report: {ex.Message}");
            }
        }
    }
}
