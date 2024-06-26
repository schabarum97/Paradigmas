﻿using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services;
using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;
using ApiWebDB.Services.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiWebDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public readonly ClienteService _service;
        private readonly ILogger _logger;

        public ClientesController(ClienteService service, ILogger<ClientesController> logger)
        {
            _service = service;
            _logger = logger;
        }
        /// <summary>
        /// Insere um novo cliente
        /// </summary>
        /// <returns>Retorna o cliente inserido</returns>
        /// <response code="200">Retorna o Json com os dados do novo cliente</response>
        /// <response code="400">Os dados enviados não são válidos</response>
        /// <response code="422">Campos obrigatórios não enviados para a atualização</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPost()]
        public ActionResult<TbCliente> Insert(ClienteDTO cliente)
        {
            try
            {
                var entity = _service.Insert(cliente);
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
        /// Atualiza os dados do cliente de acordo com seu ID
        /// </summary>
        /// <returns>Retorna os dados do cliente atualizado</returns>
        /// <response code="200">Retorna o Json com os dados do cliente</response>
        /// <response code="400">Os dados enviados não são válidos</response>
        /// <response code="404">Registro não encontrado para a atualização</response>
        /// <response code="422">Campos obrigatórios não enviados para a atualização</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpPut("{id}")]
        public ActionResult<TbCliente> Update(int id, ClienteDTO dto)
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
        /// Faz a Exclusão de um cliente de acordo com seu ID
        /// </summary>
        /// <returns>Retorna que o cliente foi deletado</returns>
        /// <response code="204">Retorna que o cliente foi deletado</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpDelete("{id}")]
        public ActionResult<TbCliente> Delete(int id)
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
        /// Retorna um determinado cliente passando o ID
        /// </summary>
        /// <returns>Retorna os dados do cliente</returns>
        /// <response code="200">Retorna o Json com os dados do cliente</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet("{id}")]
        public ActionResult<TbCliente> GetById(int id)
        {
            try
            {
                var entity =  _service.GetById(id);
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
        /// <summary>
        /// Retorna todos os cliente
        /// </summary>
        /// <returns>Retorna os dados dos cliente</returns>
        /// <response code="200">Retorna o Json com os dados dos cliente</response>
        /// <response code="500">Erro interno de servidor</response>
        [HttpGet()]
        public ActionResult<TbCliente> Get()
        {
            try
            {
                var entity = _service.Get();
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
