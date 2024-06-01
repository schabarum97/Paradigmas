using API_TF.DataBase;
using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using API_TF.Services.Parser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_TF.Services
{
    public class StockLogService
    {
        private readonly TfDbContext _dbContext;

        public StockLogService(TfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<object> GetLogByProductId(int productId)
        {
            var logs = _dbContext.TbStockLogs
                                 .Where(log => log.Productid == productId)
                                 .OrderBy(log => log.Createdat)
                                 .ToList();

            var stockLogs = new List<object>();

            foreach (var log in logs)
            {
                var product = _dbContext.TbProducts.FirstOrDefault(c => c.Id == log.Productid);
                if (product != null)
                {
                    var stockLogDTO = new
                    {
                        Id = log.Id,
                        Productid = log.Productid,
                        Qty = log.Qty,
                        Createdat = log.Createdat,
                        Barcode = product.Barcode,
                        Description = product.Description
                    };
                    stockLogs.Add(stockLogDTO);
                }
            }

            return stockLogs;
        }

        public TbStockLog Post(StockLogDTO dto)
        {
            var logEntity = StockLogParser.ToEntity(dto);

            _dbContext.TbStockLogs.Add(logEntity);
            _dbContext.SaveChanges();

            return logEntity;
        }
    }
}
