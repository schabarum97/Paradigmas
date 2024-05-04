using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services;
using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndercoController : ControllerBase
    {
        public readonly EnderecoService _service;
        private readonly ILogger _logger;
        public EndercoController(EnderecoService service, ILogger<EndercoController> logger)
        {
            _service = service;
            _logger = logger;
        }
        /// <summary>
        /// Insere um novo endereço para um cliente
        /// </summary>
        /// <returns>Retorna o endereço inserido</returns>
        /// <response code="200">Retorna o Json com os dados do novo endereço</response>
        /// <response code="400">Os dados enviados não são válidos</response>
        /// <response code="422">Campos obrigatórios não enviados para a atualização</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPost()]
        public ActionResult<TbEndereco> Insert(EnderecoDTO ender)
        {
            try
            {
                var entity = _service.Insert(ender);
                return Ok(entity);
            }
            catch (InvalidEntityException E)
            {
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };
            }
            catch (BadRequestException E)
            {
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 400
                };
            }
            catch (System.Exception E)
            {
                _logger.LogError(E.Message);
                return BadRequest(E.Message);
            }
        }
        /// <summary>
        /// Atualiza os dados do Endereço de acordo com seu Id
        /// </summary>
        /// <returns>Retorna os dados do endereço atualizado</returns>
        /// <response code="200">Retorna o Json com os dados do novo endereço</response>
        /// <response code="400">Os dados enviados não são válidos</response>
        /// <response code="404">Registro não encontrado para a atualização</response>
        /// <response code="422">Campos obrigatórios não enviados para a atualização</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPut("{id}")]
        public ActionResult<TbEndereco> Update(int id, EnderecoDTO dto)
        {
            try
            {
                var entity = _service.Update(dto, id);
                return Ok(entity);
            }
            catch (InvalidEntityException E)
            {
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 422
                };
            }
            catch (BadRequestException E)
            {
                return new ObjectResult(new { error = E.Message })
                {
                    StatusCode = 400
                };
            }
            catch (NotFoundException E)
            {
                return NotFound(E.Message);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Faz a Exclusão de um Endereço de acordo com seu ID
        /// </summary>
        /// <returns>Retorna o Endereço Deletado</returns>
        /// <response code="204">Retorna o Endereço Deletado</response>
        /// <response code="404">Endereço não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpDelete("{id}")]
        public ActionResult<TbEndereco> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
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
        /// <summary>
        /// Retorna os dados do Endereço de acordo com o ID do cliente
        /// </summary>
        /// <returns>Retorna os dados do endereço</returns>
        /// <response code="200">Retorna o Json com os dados do endereço</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet("{id}")]
        public ActionResult<TbEndereco> GetEnder(int id)
        {
            try
            {
                var entity = _service.GetEnder(id);
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
