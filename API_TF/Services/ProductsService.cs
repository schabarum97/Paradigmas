using API_TF.DataBase;
using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using API_TF.Services.Execptions;
using API_TF.Services.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_TF.Services
{
    public class ProductsService
    {
        private readonly TfDbContext _dbContext;
        public ProductsService(TfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TbProduct GetById(int id)
        {
            return _dbContext.TbProducts.FirstOrDefault(c => c.Id == id);
        }

        public TbProduct GetByBarcode(string barcode)
        {
            var existingEntity = _dbContext.TbProducts
                                 .Where(c => c.Barcode == barcode)
                                 .FirstOrDefault();
            
            if (existingEntity == null)
            {
                throw new NotFoundException("Não encontrada nenhum produto com o barcode passado");
            }
            return existingEntity;
        }

        public IEnumerable<TbProduct> GetByDesc(string desc)
        {
            var existingEntity = _dbContext.TbProducts
                                            .ToList()
                                            .Where(c => c.Description.ToUpper().Contains(desc.ToUpper()));
            return existingEntity;
        }

        public TbProduct Post (ProductDTO dto)
        {
            var product = ProductParser.ToEntity(dto);

            _dbContext.Add(product);
            _dbContext.SaveChanges();

            return product;
        }

        public TbProduct Put(ProductDTO dto, int id)
        {
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Id do produto que foi passado não existe");
            }
            var product = ProductParser.ToEntity(dto);

            existingEntity.Description = product.Description;
            existingEntity.Barcode = product.Barcode;
            existingEntity.Barcodetype = product.Barcodetype;
            existingEntity.Price = product.Price;
            existingEntity.Costprice = product.Costprice;

            _dbContext.Update(existingEntity);
            _dbContext.SaveChanges();

            return product;
        }

        public void Patch(int id, int qtd)
        {
            var existingEntity = GetById(id);
            if (existingEntity == null)
            {
                throw new NotFoundException("Id do produto que foi passado não existe");
            }
            existingEntity.Stock = qtd;

            _dbContext.Update(existingEntity);
            _dbContext.SaveChanges();
        }
    }
}
