using System;

namespace API_TF.Services.DTO
{
    public class StockLogDTO
    {
        public int Id { get; set; }
        public int Productid { get; set; }
        public int Qty { get; set; }
        public DateTime Createdat { get; set; }
    }
}
