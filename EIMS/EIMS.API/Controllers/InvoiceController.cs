using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            if (request == null)
                return BadRequest("Invalid invoice data.");

            var invoice = await _invoiceService.CreateInvoiceAsync(request);

            return Ok(new
            {
                message = "Invoice created successfully.",
                invoiceID = invoice.InvoiceID,
                customerID = invoice.CustomerID,
                totalAmount = invoice.TotalAmount,
                totalInWords = invoice.TotalAmountInWords
            });
        }
    }
}
