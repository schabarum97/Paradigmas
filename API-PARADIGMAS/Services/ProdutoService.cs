using API_PARADIGMAS.Database;
using API_PARADIGMAS.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_PARADIGMAS.Services
{
    public class NotFoundException : Exception
    {
        public NotFoundException()  { }
    }
    public class ProdutoService
    {
        private readonly ContextDB _contextDB;
        public ProdutoService(ContextDB contextDB) 
        {
            _contextDB = contextDB;
        }
        public List<Produto> GetAll() 
        {
            return _contextDB.Produto;
        }
        
        public Produto GetbyId(int codigo) 
        { 
           try
            {
                return _contextDB.Produto.
                Where(p => p.Codigo == codigo).
                First();
            } catch
            {
                throw new NotFoundException();
            }
            
        }
    }
}
