using API_Printing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API_Printing.Reports;

namespace API_Printing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {

        private readonly BillingContext _context;

        public ConfigurationsController(BillingContext context)
        {
            _context = context;
        }


        // GET: api/Configurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Configurations>>> GetConfigurations()
        {
            return await _context.Configurations.ToListAsync();
        }

        // GET: api/Configurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Configurations>> GetConfiguration(int id)
        {
            var config = await _context.Configurations.FindAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            return config;
        }




        [HttpGet("ReportConfigurations")]
        public IActionResult GetReportConfigurations()
        {
            var report = new Report2();
            using var stream = new MemoryStream();
            report.ExportToPdf(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream.ToArray(), "application/pdf", "ElectricityBill.pdf");
        }



    }
}
