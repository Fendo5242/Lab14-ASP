using Lab13.Models;
using Lab13.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lab13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        private readonly StoreContext _context;

        public InvoiceDetailsController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost("insert-details")]
        public IActionResult InsertInvoiceDetails([FromBody] InvoiceDetailInsertRequest request)
        {
            if (request.Details == null || request.Details.Count == 0)
            {
                return BadRequest("La lista de detalles de factura no puede estar vacía.");
            }

            var invoice = _context.Invoices.FirstOrDefault(i => i.InvoiceID == request.InvoiceId);
            if (invoice == null)
            {
                return NotFound(new { message = $"Factura con ID {request.InvoiceId} no encontrada." });
            }

            foreach (var detailInsert in request.Details)
            {
                var detail = new Detail
                {
                    InvoiceID = request.InvoiceId,
                    ProductID = detailInsert.ProductID,
                    Amount = detailInsert.Amount,
                    Price = detailInsert.Price,
                    SubTotal = detailInsert.Amount * detailInsert.Price
                };

                _context.Details.Add(detail);
            }

            _context.SaveChanges();

            return Ok(new { message = $"Se han insertado {request.Details.Count} detalles para la factura ID {request.InvoiceId}." });
        }
    }
}
