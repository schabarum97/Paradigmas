using API_TF.DataBase.Models;
using API_TF.Services;
using API_TF.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

namespace API_TF.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PromotionsController : ControllerBase
    {
        public readonly PromotionsService _service;
        private readonly ILogger _logger;
        public PromotionsController(PromotionsService service, ILogger<PromotionsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost()]
        public ActionResult<TbPromotion> Post(PromotionDTO promotion)
        {
            try
            {
                var entity = _service.Post(promotion);
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
