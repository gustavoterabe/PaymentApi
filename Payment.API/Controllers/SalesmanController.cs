using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;

namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesmanController : ControllerBase
    {
        private readonly IBaseRepository<Salesman> _salesmanRepository;

        public SalesmanController(IBaseRepository<Salesman> salesmanRepository)
        {
            _salesmanRepository = salesmanRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSalesman(Guid id)
        {
            Salesman salesman;

            try
            {
                salesman = _salesmanRepository.GetByIdOrThrow(id);
            }
            catch (Exception ex)
            {
                return ex.InnerException switch
                {
                    ObjectNotFoundException => NotFound(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, ex.Message)
                };
            }
            
            return Ok(salesman);
        }
        
        [HttpPost]
        public IActionResult CreateSalesman(Salesman newSalesman)
        {
            Salesman salesman;
            try
            {
                salesman = _salesmanRepository.Insert(newSalesman);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return CreatedAtAction(nameof(GetSalesman), new { id = salesman.Id }, salesman);
        }
    }
}
