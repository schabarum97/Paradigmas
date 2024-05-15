using API_TF.DataBase;
using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using API_TF.Services.Parser;
using API_TF.Services.Validate;
using System.Linq;

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

    }
}
