using Lab13.Models;
using Lab13.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesCustomController : ControllerBase
    {
        private readonly StoreContext _context;

        public InvoicesCustomController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public IActionResult InsertInvoice([FromBody] InvoiceRequestInsert request)
        {
            if (request == null)
            {
                return BadRequest("Request invalido.");
            }

            var newInvoice = new Invoice
            {
                CustomerID = request.IdCustomer,
                Date = request.Date,
                InvoiceNumber = request.InvoiceNumber,
                Total = request.Total
            };

            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();

            return Ok(new { message = $"Factura '{request.InvoiceNumber}' insertada exitosamente para el cliente ID {request.IdCustomer}." });
        }

        [HttpPost("insert-multiple")]
        public IActionResult InsertMultipleInvoices([FromBody] InvoiceInsertRequest request)
        {
            if (request.Invoices == null || request.Invoices.Count == 0)
            {
                return BadRequest("La lista de facturas no puede estar vacía.");
            }

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == request.CustomerId);
            if (customer == null)
            {
                return NotFound(new { message = $"Cliente con ID {request.CustomerId} no encontrado." });
            }

            foreach (var invoiceDetail in request.Invoices)
            {
                var newInvoice = new Invoice
                {
                    CustomerID = request.CustomerId,
                    Date = invoiceDetail.Date,
                    InvoiceNumber = invoiceDetail.InvoiceNumber,
                    Total = invoiceDetail.Total
                };
                _context.Invoices.Add(newInvoice);
            }

            _context.SaveChanges();

            return Ok(new { message = $"Se han insertado {request.Invoices.Count} facturas para el cliente ID {request.CustomerId}." });
        }
    }
}