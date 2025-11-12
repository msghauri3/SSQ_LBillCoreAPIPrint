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
    public class ElectricityBillsNetMeterController : ControllerBase
    {
        [HttpGet("GetNetMeterBill")]
        public async Task<IActionResult> GetNetMeterBill(
            [FromQuery] string? block,
            [FromQuery] string? Category,
            [FromQuery] string? month,
            [FromQuery] string? year,
            [FromQuery] string? Project,
            [FromServices] BillingService billingService)
        {
            try
            {
                // ✅ Initialize the DevExpress report
                var report = new NetMeteringBill();

                if (report == null)
                    return StatusCode(500, "Failed to initialize the ElectricityBillNetMeter report.");

                // ✅ Helper function to safely set parameters
                void SetParameter(string paramName, string? value)
                {
                    var param = report.Parameters[paramName];
                    if (param != null)
                    {
                        if (!string.IsNullOrWhiteSpace(value))
                            param.Value = value;
                        else
                            param.Value = DBNull.Value; // important fix

                        param.Visible = false;
                    }
                }


                // ✅ Assign parameters from query
                SetParameter("Category", Category);
                SetParameter("Block", block);
                SetParameter("BillingMonth", month);
                SetParameter("BillingYear", year);
                SetParameter("Project", Project);

                // ✅ Optional: extend SQL timeout (default = 30s)
                if (report.DataSource is SqlDataSource sqlDataSource)
                {
                    sqlDataSource.ConnectionOptions.CommandTimeout = 120;
                }

                // ✅ Export the report to PDF
                using var stream = new MemoryStream();
                report.ExportToPdf(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream.ToArray(), "application/pdf", "ElectricityBillNetMeter.pdf");
            }
            catch (Exception ex)
            {
                // Return detailed message for debugging
                return StatusCode(500, $"Error generating Net Meter report: {ex.Message}");
            }
        }
    }
}
