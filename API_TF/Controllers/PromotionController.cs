using API_TF.DataBase.Models;
using API_TF.Services;
using API_TF.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Cria uma nova promoção.
        /// </summary>
        /// <param name="promotion">Os dados da nova promoção.</param>
        /// <returns>Os detalhes da promoção criada.</returns>
        /// <response code="200">Retorna o JSON com os detalhes da promoção criada.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TbPromotion), 200)]
        [ProducesResponseType(400)]
        public ActionResult<TbPromotion> Post(PromotionDTO promotion)
        {
            try
            {
                var entity = _service.Post(promotion);
                return Ok(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualiza uma promoção existente.
        /// </summary>
        /// <param name="id">O ID da promoção a ser atualizada.</param>
        /// <param name="promotion">Os novos dados da promoção.</param>
        /// <returns>Os detalhes da promoção atualizada.</returns>
        /// <response code="200">Retorna o JSON com os detalhes da promoção atualizada.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TbPromotion), 200)]
        [ProducesResponseType(400)]
        public ActionResult<TbPromotion> Update(int id, PromotionDTO promotion)
        {
            try
            {
                var entity = _service.Update(id, promotion);
                return Ok(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtém as promoções de um produto dentro de um determinado período.
        /// </summary>
        /// <param name="productId">O ID do produto.</param>
        /// <param name="startDate">A data de início do período.</param>
        /// <param name="endDate">A data de término do período.</param>
        /// <returns>Uma lista de promoções para o produto dentro do período especificado.</returns>
        /// <response code="200">Retorna a lista de promoções.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpGet("{productId}/{startDate}/{endDate}")]
        [ProducesResponseType(typeof(IEnumerable<TbPromotion>), 200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<TbPromotion>> GetPromotionsByProductAndPeriod(int productId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var promotions = _service.GetPromotionsByProductAndPeriod(productId, startDate, endDate);
                return Ok(promotions);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
