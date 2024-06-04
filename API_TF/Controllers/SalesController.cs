using API_TF.DataBase.Models;
using API_TF.Services;
using API_TF.Services.DTO;
using API_TF.Services.Execptions;
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
        /// <param name="dtoList"></param>
        /// <returns>Os detalhes da venda registrada.</returns>
        /// <response code="200">Retorna o JSON com os detalhes da venda registrada.</response>
        /// <response code="400">Indica que houve um erro de validação nos dados da venda ou que o estoque é insuficiente.</response>
        /// <response code="404">Indica que o produto com o ID especificado não foi encontrado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TbSale), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] List<SaleDTO> dtoList)
        {
            try
            {
                var sales = _service.Post(dtoList);
                return Ok(sales);
            }
            catch (InvalidEntityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InsufficientStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
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
        /// <summary>
        /// Obtém um relatório de vendas por período.
        /// </summary>
        /// <param name="startDate">A data de início do período.</param>
        /// <param name="endDate">A data de fim do período.</param>
        /// <returns>Uma lista de relatórios de vendas agrupados por código da venda.</returns>
        /// <response code="200">Indica que o relatório de vendas foi retornado com sucesso.</response>
        /// <response code="400">Indica que as datas de início e fim não foram fornecidas ou são inválidas.</response>
        /// <response code="404">Indica que não foram encontradas vendas no período especificado.</response>
        /// <response code="500">Indica que ocorreu um erro interno no servidor.</response>
        [HttpGet("report")]
        public ActionResult<List<SalesReportDTO>> GetSalesReport(DateTime startDate, DateTime endDate)
        {

            try
            {
                var report = _service.GetSalesReportByPeriod(startDate, endDate);
                return Ok(report);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
