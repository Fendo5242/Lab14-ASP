using Lab13.Models;
using Lab13.Models.Request;
using Lab13.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersCustomController : ControllerBase
    {
        private readonly StoreContext _context;

        public CustomersCustomController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public IActionResult InsertCustomer([FromBody] CustomerRequestInsert request)
        {
            var newCustomer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DocumentNumber = request.DocumentNumber,
                IsCustomer = true
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();

            return Ok(new { message = $"Cliente '{request.FirstName} {request.LastName}' insertado exitosamente." });
        }

        [HttpPost("delete")]
        public IActionResult DeleteCustomer([FromBody] CustomerRequestDelete request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == request.Id);
            if (customer == null)
            {
                return NotFound(new { message = $"Cliente con ID {request.Id} no encontrado." });
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(new { message = $"Cliente con ID {request.Id} eliminado exitosamente." });
        }


        [HttpPost("update-document")]
        public IActionResult UpdateCustomerDocument([FromBody] CustomersRequestUpdate request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID == request.Id);
            if (customer == null)
            {
                return NotFound(new { message = $"Cliente con ID {request.Id} no encontrado." });
            }

            customer.DocumentNumber = request.DocumentNumber;

            _context.SaveChanges();

            return Ok(new { message = $"Documento del cliente con ID {request.Id} actualizado exitosamente." });
        }
    }
}