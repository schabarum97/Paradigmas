using API_TF.DataBase.Models;
using API_TF.Services;
using API_TF.Services.DTO;
using API_TF.Services.Execptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Mime;

namespace API_TF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ProductsController : ControllerBase
    {
        public readonly ProductsService _service;
        private readonly ILogger _logger;
        public ProductsController(ProductsService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtém um produto pelo código de barras.
        /// </summary>
        /// <param name="barcode">O código de barras do produto.</param>
        /// <returns>Os detalhes do produto.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto.</response>
        /// <response code="404">Produto não encontrado.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("barcode/{barcode}")]
        [ProducesResponseType(typeof(TbProduct), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
                return StatusCode(500, new { error = e.Message });
            }
        }

        /// <summary>
        /// Obtém um produto pela descrição.
        /// </summary>
        /// <param name="desc">A descrição do produto.</param>
        /// <returns>Os detalhes do produto.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet("description/{desc}")]
        [ProducesResponseType(typeof(IEnumerable<TbProduct>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<TbProduct>> GetByDesc(string desc)
        {
            try
            {
                var entity = _service.GetByDesc(desc);
                return Ok(entity);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, new { error = e.Message });
            }
        }

        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="product">Os dados atualizados do produto.</param>
        /// <returns>Os detalhes do produto atualizado.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto atualizado.</response>
        /// <response code="400">Os dados enviados não são válidos.</response>
        /// <response code="404">Produto não encontrado.</response>
        /// <response code="422">Campos obrigatórios não enviados para a atualização.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TbProduct), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public ActionResult<TbProduct> Put(int id, ProductDTO product)
        {
            try
            {
                var entity = _service.Put(product, id);
                return Ok(entity);
            }
            catch (InvalidEntityException E)
            {
                return StatusCode(422, new { error = E.Message });
            }
            catch (BadRequestException E)
            {
                return StatusCode(400, new { error = E.Message });
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, new { error = e.Message });
            }
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="product">Os dados do novo produto.</param>
        /// <returns>Os detalhes do produto criado.</returns>
        /// <response code="200">Retorna o JSON com os detalhes do produto criado.</response>
        /// <response code="400">Os dados enviados não são válidos.</response>
        /// <response code="422">Campos obrigatórios não enviados para a criação.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost()]
        [ProducesResponseType(typeof(TbProduct), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public ActionResult<TbProduct> Post(ProductDTO product)
        {
            try
            {
                var entity = _service.Post(product);
                return Ok(entity);
            }
            catch (InvalidEntityException E)
            {
                return StatusCode(422, new { error = E.Message });
            }
            catch (BadRequestException E)
            {
                return StatusCode(400, new { error = E.Message });
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return StatusCode(500, new { error = E.Message });
            }
        }
    }
}
