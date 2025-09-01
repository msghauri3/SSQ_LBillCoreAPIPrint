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
            //var report = new PrintEBill01();
            //using var stream = new MemoryStream();
            //report.ExportToPdf(stream);
            //stream.Seek(0, SeekOrigin.Begin);
            //return File(stream.ToArray(), "application/pdf", "ElectricityBill.pdf");


            // 1. Query top 10 records from EF Core
            var bills = _context.EBills
                                .OrderByDescending(e => e.Uid) // Or another column
                                .Take(10)
                                .ToList();

            // 2. Create report instance
            var report = new PrintEBill01();

            // 3. Assign data source
            report.DataSource = bills;
            report.DataMember = null; // Use root-level binding

            // 4. Export to PDF
            using var stream = new MemoryStream();
            report.ExportToPdf(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/pdf", "ElectricityBill.pdf");

        }




    }
}
