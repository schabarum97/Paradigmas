using System;
using System.Collections.Generic;

namespace API_TF.Services.DTO
{
    public class SalesReportDTO
    {
        public string SaleCode { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleProductDTO> Products { get; set; }
    }
}
