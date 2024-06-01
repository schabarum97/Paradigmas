using API_TF.DataBase;
using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using API_TF.Services.Parser;
using API_TF.Services.Validate;
using System;
using System.Linq;
using System.Collections.Generic;

namespace API_TF.Services
{
    public class PromotionsService
    {
        private readonly TfDbContext _dbContext;
        public PromotionsService(TfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TbPromotion GetById(int id)
        {
            return _dbContext.TbPromotions.FirstOrDefault(c => c.Id == id);
        }

        public TbPromotion Post(PromotionDTO dto)
        {
            var promotion = PromotionParser.ToEntity(dto);

            _dbContext.Add(promotion);
            _dbContext.SaveChanges();

            return promotion;
        }

        public TbPromotion Update(int id, PromotionDTO dto)
        {
            var existingPromotion = _dbContext.TbPromotions.FirstOrDefault(c => c.Id == id);
            if (existingPromotion == null) throw new Exception("Promotion not found");

            existingPromotion.Startdate = dto.Startdate;
            existingPromotion.Enddate = dto.Enddate;
            existingPromotion.Promotiontype = dto.Promotiontype;
            existingPromotion.Productid = dto.Productid;
            existingPromotion.Value = dto.Value;

            _dbContext.SaveChanges();

            return existingPromotion;
        }

        public IEnumerable<TbPromotion> GetPromotionsByProductAndPeriod(int productId, DateTime startDate, DateTime endDate)
        {
            return _dbContext.TbPromotions
                             .Where(p => p.Productid == productId &&
                                         p.Startdate >= startDate &&
                                         p.Enddate <= endDate)
                             .ToList();
        }
    }
}
