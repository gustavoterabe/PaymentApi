using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductController(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            Product product;
            
            try
            {
                product = _productRepository.GetByIdOrThrow(id);
            }
            catch (Exception ex)
            {
                return ex.InnerException switch
                {
                    ObjectNotFoundException => NotFound(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, ex.Message)
                };
            }
            
            return Ok(product);
        }
        
        [HttpPost]
        public IActionResult CreateProduct(Product newProduct)
        {
            Product product;
            try
            {
                product = _productRepository.Insert(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}
