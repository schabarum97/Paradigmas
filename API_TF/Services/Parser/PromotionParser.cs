using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using Newtonsoft.Json.Linq;

namespace API_TF.Services.Parser
{
    public class PromotionParser
    {
        public static TbPromotion ToEntity(PromotionDTO dto)
        {
            return new TbPromotion
            {
                Startdate = dto.Startdate,
                Enddate = dto.Enddate,
                Promotiontype = dto.Promotiontype,
                Productid = dto.Productid,
                Value = dto.Value
            };
        }
    }
}
