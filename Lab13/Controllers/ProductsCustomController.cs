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
    public class ProductsCustomController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsCustomController(StoreContext context)
        {
            _context = context;
        }

        [HttpPost("insert")]
        public async Task<ActionResult<ProductResponseInsert>> PostProduct([FromBody] ProductRequestInsert request)
        {
            if (request == null)
            {
                return BadRequest("Request invalido.");
            }

            var product = new Product
            {
                Name = request.Name,
                Price = (float)request.Price,
                IsActive = true
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var response = new ProductResponseInsert
            {
                ProductId = product.ProductID,
                Name = product.Name,
                Price = (decimal)product.Price,
                IsActive = product.IsActive
            };

            return Ok(response);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] ProductRequestDelete request)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductID == request.ProductId);

                if (product == null)
                {
                    return NotFound(new { message = $"Producto con ID {request.ProductId} no encontrado" });
                }

                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok(new { message = $"Producto con ID {request.ProductId} eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error interno del servidor: {ex.Message}" });
            }
        }

        [HttpPost("update-price")]
        public IActionResult UpdateProductPrice([FromBody] ProductPriceUpdateRequest request)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == request.Id);
            if (product == null)
            {
                return NotFound(new { message = $"Producto con ID {request.Id} no encontrado." });
            }

            product.Price = request.Price;

            _context.SaveChanges();

            return Ok(new { message = $"Precio del producto con ID {request.Id} actualizado a {request.Price}. Detalles adicionales recibidos y registrados." });
        }

        [HttpPost("delete-multiple")]
        public IActionResult DeleteMultipleProducts([FromBody] ProductListDeleteRequest request)
        {
            if (request.ProductIds == null || request.ProductIds.Count == 0)
            {
                return BadRequest("La lista de IDs de productos no puede estar vacía.");
            }

            var productsToDelete = _context.Products.Where(p => request.ProductIds.Contains(p.ProductID)).ToList();

            if (productsToDelete.Count == 0)
            {
                return NotFound("No se encontraron productos para eliminar.");
            }

            _context.Products.RemoveRange(productsToDelete);
            _context.SaveChanges();

            return Ok(new { message = $"Se han eliminado {productsToDelete.Count} productos exitosamente." });
        }
    }
}
