using API_Printing.Models;
using API_Printing.Reports;
using DevExpress.XtraRichEdit.Import.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Printing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EBillController : Controller
    {
        private readonly BillingContext _context;
        public EBillController(BillingContext context)
        {
            _context = context;
        }


        // GET: api/Configurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EBills>>> GetEBills()
        {
            var bills = await _context.EBills
                                      .OrderByDescending(e => e.Uid)
                                      .Take(10)
                                      .ToListAsync();

            return Ok(bills);

        }


        [HttpGet("PrintEBills")]
        public IActionResult PrintEBills()
        {
            // 1. Query record by BTNo
            var bill = _context.EBills
                               .FirstOrDefault(e => e.BTNo == "BTL-10743");

            if (bill == null)
            {
                return NotFound("Bill not found for BTNo BTL-10743");
            }

            // 2. Create report instance
            var report = new PrintEBill02();

            // 3. Assign data source (single record wrapped in a list)
            report.DataSource = new List<EBills> { bill };
            report.DataMember = null;

            // 4. Export to PDF
            using var stream = new MemoryStream();
            report.ExportToPdf(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/pdf", "ElectricityBill.pdf");
        }





    }
}
