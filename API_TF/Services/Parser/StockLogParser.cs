using API_TF.DataBase.Models;
using API_TF.Services.DTO;

namespace API_TF.Services.Parser
{
    public class StockLogParser
    {
        public static TbStockLog ToEntity(StockLogDTO dto)
        {
            return new TbStockLog
            {
                Productid = dto.Productid,
                Qty = dto.Qty,
                Createdat = dto.Createdat
            };
        }
    }
}
