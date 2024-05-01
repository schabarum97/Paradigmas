using ApiWebDB.BaseDados;
using ApiWebDB.BaseDados.Models;
using ApiWebDB.Controllers;
using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;
using ApiWebDB.Services.Parser;
using ApiWebDB.Services.Validate;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ApiWebDB.Services
{
    public class EnderecoService
    {
        private readonly ApiDbContext _dbContext;
        private readonly ILogger _logger;

        public EnderecoService(ApiDbContext dbcontext, ILogger<ClientesController> logger)
        {
            _dbContext = dbcontext;
            _logger = logger;
        }
        public TbEndereco Insert(EnderecoDTO dto)
        {
            if (!EnderecoValidate.Execute(dto))
                return null;

            var entity = EnderecoParser.ToEntity(dto);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public TbEndereco Update(EnderecoDTO dto, int id)
        {
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Registro não existe");
            }

            if (!EnderecoValidate.Execute(dto))
                return null;
            
            var entity = EnderecoParser.ToEntity(dto);

            var EnderecoId = GetById(id);

            EnderecoId.Cep = entity.Cep;
            EnderecoId.Logradouro = entity.Logradouro;
            EnderecoId.Numero = entity.Numero;
            EnderecoId.Complemento = entity.Complemento;
            EnderecoId.Bairro = entity.Bairro;
            EnderecoId.Cidade = entity.Cidade;
            EnderecoId.Uf = entity.Uf;
            EnderecoId.Status = entity.Status;

            _dbContext.Update(EnderecoId);
            _dbContext.SaveChanges();
            return entity;
        }

        public TbEndereco GetById(int id)
        {
            var existingEntity = _dbContext.TbEnderecos.FirstOrDefault(c => c.Id == id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Registro não existe");
            }
            return existingEntity;
        }

        public void Delete(int id)
        {
            var existingEntity = GetById(id);

            if (existingEntity == null)
            {
                throw new NotFoundException("Registro não existe");
            }
            _dbContext.Remove(existingEntity);
            _dbContext.SaveChanges();
        }

        public TbEndereco GetEnder(int id)
        {
            var existingEntity = _dbContext.TbEnderecos.FirstOrDefault(c => c.Clienteid == id);
            
            return existingEntity;
        }
    }
}
