using API_PARADIGMAS.Database.Models;
using API_PARADIGMAS.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace API_PARADIGMAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        [HttpGet()]
        public ActionResult<List<Produto>> GetAll()
        {
            return Ok(_produtoService.GetAll());    
        }

        [HttpGet(":codigo")]
        public ActionResult<Produto> GetbyId(int codigo)
        {
            try
            {
                var produto = _produtoService.GetbyId(codigo);

                return Ok(produto);
            }
            catch (NotFoundException)
            {
                return NotFound("Produto não encontrado");
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um problema ao acessar produto." + e.Message);
            }
        }
    }
}
