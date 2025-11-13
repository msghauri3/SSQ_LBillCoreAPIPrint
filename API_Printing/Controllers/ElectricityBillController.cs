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
    public class ElectricityBillController : ControllerBase
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
                var report = new ElectricityBill();
                if (report == null)
                    return StatusCode(500, "Failed to initialize the ElectricityBill report.");

                // Prevent DevExpress from running before parameters are set
                report.RequestParameters = false;

                // Helper to safely set report parameters
                void SetParameter(string paramName, string? value)
                {
                    var param = report.Parameters[paramName];
                    if (param != null)
                    {
                        if (!string.IsNullOrWhiteSpace(value))
                            param.Value = value;
                        else
                            param.Value = DBNull.Value; // send SQL NULL

                        param.Visible = false;
                    }
                }

                // Assign all filters
                SetParameter("Category", Category);
                SetParameter("Block", block);
                SetParameter("BillingMonth", month);
                SetParameter("BillingYear", year);
                SetParameter("Project", Project);

                // Increase SQL command timeout
                if (report.DataSource is SqlDataSource sqlDataSource)
                {
                    sqlDataSource.ConnectionOptions.CommandTimeout = 300; // 5 minutes
                }

                // Export report to PDF
                using var stream = new MemoryStream();
                report.ExportToPdf(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream.ToArray(), "application/pdf", "ElectricityBill.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating report: {ex.Message}");
            }
        }
    }
}
