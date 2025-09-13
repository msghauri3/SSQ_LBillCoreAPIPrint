using API_Printing.Models;
using API_Printing.Reports;
using DevExpress.DataProcessing.InMemoryDataProcessor.GraphGenerator;
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
        public async Task<IActionResult> PrintEBills()
        {
            var bills = await _context.EBills
                                      //.Where(e => e.InvoiceNo == "20250307373")
                                      .Where(e => e.BTNo == "BTL-10743")
                                      .ToListAsync();

            if (bills == null || bills.Count == 0)
            {
                return NotFound("Bill(s) not found.");
            }

            // Create report instance
            var report = new PrintEBill02
            {
                DataSource = bills,
                DataMember = null
            };

            // Export to single PDF with multiple pages
            using var stream = new MemoryStream();
            report.ExportToPdf(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/pdf", "ElectricityBills.pdf");
        }







    }
}
