using API_TF.DataBase.Models;
using API_TF.Services.DTO;

namespace API_TF.Services.Parser
{
    public class SaleParser
    {
        public static TbSale ToEntity(SaleDTO dto)
        {
            return new TbSale
            {
                Productid = dto.Productid,
                Qty = dto.Qty
            };
        }
    }
}
