using API_TF.DataBase.Models;
using API_TF.Services;
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
        [HttpGet()]
        public ActionResult<TbProduct> GetByBarcode(string barcode)
        {
            try
            {
                var entity = _service.GetByBarcode(barcode);
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
    }
}
