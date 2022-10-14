
using System.Data;
using System.Data.Entity.Core;
using Microsoft.AspNetCore.Mvc;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;
using Payment.Domain.Utils.Constants;
using Payment.Domain.ViewModel.Sale;


namespace Payment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        
        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSale(Guid id)
        {
            Sale sale;
            try
            {
                sale = _saleService.GetById(id);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is ObjectNotFoundException)
                    return NotFound(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(sale);
        }
        
        [HttpPost]
        public IActionResult CreateSale(CreateSaleViewModel createSaleViewModel)
        {
            Sale sale;
            try
            {
                sale = _saleService.CreateSale(createSaleViewModel);
            }
            catch (Exception ex)
            {
                return ex.InnerException switch
                {
                    InvalidConstraintException => BadRequest(ex.InnerException.Message),
                    ObjectNotFoundException => NotFound(ex.InnerException.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, ex)
                };
            }

            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }

        [HttpPut]
        [Route("{id}/status/{status}")]
        public IActionResult ChangeSaleStatus(Guid id, SaleStatusEnum saleStatus)
        {
            try
            {
                _saleService.ChangeSaleStatus(id, saleStatus);
            }
            catch (Exception ex)
            {
                return ex.InnerException switch
                {
                    InvalidConstraintException => BadRequest(ex.InnerException.Message),
                    ObjectNotFoundException => NotFound(ex.InnerException.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
                };
            }
            return Ok($"Status updated to {saleStatus} successfully!");
        }
    }
}
