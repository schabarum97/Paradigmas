using ApiWebDB.BaseDados;
using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services.DTOs;
using ApiWebDB.Services.Exceptions;
using ApiWebDB.Services.Parser;
using ApiWebDB.Services.Validate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ApiWebDB.Services
{
    public class ClienteService
    {
        private readonly ApiDbContext _dbContext;

        public ClienteService(ApiDbContext dbcontext) 
        {
            _dbContext = dbcontext;
        }
        
        public TbCliente Insert(ClienteDTO dto) 
        {
            if (!ClienteValidate.Execute(dto))
                return null;

            var entity = ClienteParser.ToEntity(dto);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public TbCliente Update(ClienteDTO dto, int id)
        {
            if (!ClienteValidate.Execute(dto))
                return null;


            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Registro não existe");
            }

            var entity = ClienteParser.ToEntity(dto);

            var ClienteById = GetById(id);

            ClienteById.Nome = entity.Nome;
            ClienteById.Nascimento = entity.Nascimento;
            ClienteById.Telefone = entity.Telefone;
            ClienteById.Documento = entity.Documento;
            ClienteById.Tipodoc = entity.Tipodoc;
            ClienteById.Alteradoem = System.DateTime.Now;


            _dbContext.Update(ClienteById);
            _dbContext.SaveChanges();

            return entity;
        }

        public TbCliente GetById(int id)
        {
            return _dbContext.TbClientes.FirstOrDefault(c => c.Id == id);
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

    }
}
