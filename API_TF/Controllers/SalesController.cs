using API_TF.DataBase.Models;
using API_TF.Services;
using API_TF.Services.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Mime;

namespace API_TF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _service;
        private readonly ILogger _logger;

        public SalesController(SalesService service, ILogger<SalesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Registra uma nova venda.
        /// </summary>
        /// <param name="sale">Os dados da nova venda.</param>
        /// <returns>Os detalhes da venda registrada.</returns>
        /// <response code="200">Retorna o JSON com os detalhes da venda registrada.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TbSale), 200)]
        [ProducesResponseType(400)]
        public ActionResult<TbSale> Post(SaleDTO sale)
        {
            try
            {
                var entity = _service.Post(sale);
                return Ok(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtém os detalhes de uma venda pelo seu código.
        /// </summary>
        /// <param name="id">O código da venda.</param>
        /// <returns>Os detalhes da venda.</returns>
        /// <response code="200">Retorna o JSON com os detalhes da venda.</response>
        /// <response code="404">Venda não encontrada.</response>
        /// <response code="400">Erro ao processar a solicitação.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TbSale), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult<TbSale> GetById(string id)
        {
            try
            {
                var sale = _service.GetById(id);
                if (sale == null)
                    return NotFound("Sale not found");

                return Ok(sale);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
