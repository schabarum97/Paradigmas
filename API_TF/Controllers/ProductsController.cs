using API_TF.DataBase.Models;
using API_TF.Services;
using API_TF.Services.DTO;
using API_TF.Services.Execptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_TF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly ProductsService _service;
        private readonly ILogger _logger;
        public ProductsController(ProductsService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("barcode/{barcode}")]
        public ActionResult<TbProduct> GetByBarcode(string barcode)
        {
            try
            {
                var entity = _service.GetByBarcode(barcode);
                return Ok(entity);
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return new ObjectResult(new { error = e.Message })
                {
                    StatusCode = 500
                };
            }
        }
        [HttpGet("description/{desc}")]
        public ActionResult<TbProduct> GetByDesc(string desc)
        {
            try
            {
                var entity = _service.GetByDesc(desc);
                return Ok(entity);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return new ObjectResult(new { error = e.Message })
                {
                    StatusCode = 500
                };
            }
        }
        [HttpPut("{id}")]
        public ActionResult<TbProduct> Put (int id, ProductDTO product)
        {
            try
            {
                var entity = _service.Put(product, id);
                return Ok(entity);
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return new ObjectResult(new { error = e.Message })
                {
                    StatusCode = 500
                };
            }
        }
        [HttpPost()]
        public ActionResult<TbProduct> Post(ProductDTO product)
        {
            try
            {
                var entity = _service.Post(product);
                return Ok(entity);
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }
    }
}
