using API_TF.DataBase;
using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using API_TF.Services.Execptions;
using API_TF.Services.Parser;
using System;
using System.Linq;

namespace API_TF.Services
{
    public class SalesService
    {
        private readonly TfDbContext _dbContext;
        private readonly ProductsService _productsService;
        private readonly StockLogService _stockLogService;

        public SalesService(TfDbContext dbContext, ProductsService productsService, StockLogService stockLogService)
        {
            _dbContext = dbContext;
            _productsService = productsService;
            _stockLogService = stockLogService;
        }

        public TbSale GetById(string id)
        {
            return _dbContext.TbSales.FirstOrDefault(s => s.Code == id);
        }

        public TbSale Post(SaleDTO dto)
        {
            var product = _productsService.GetById(dto.Productid);
            if (product == null)
                throw new NotFoundException("Produto não existe");

            if (product.Stock < dto.Qty)
                throw new Exception("Estoque insuficiente para a movimentação");

            var promotions = _dbContext.TbPromotions
                                        .Where(p => p.Productid == dto.Productid &&
                                                    p.Startdate <= dto.Createat &&
                                                    p.Enddate >= dto.Createat)
                                        .OrderBy(p => p.Promotiontype)
                                        .ToList();

            decimal discount = 0;
            decimal precoDesconto = product.Price;

            foreach (var promotion in promotions.Where(p => p.Promotiontype == 0))
            {
                var percentDiscount = precoDesconto * (promotion.Value / 100);
                discount += percentDiscount;
                precoDesconto -= percentDiscount;
            }

            foreach (var promotion in promotions.Where(p => p.Promotiontype == 1))
            {
                discount += promotion.Value;
            }
            Console.WriteLine(discount);
            dto.Discount = discount;
            dto.Price = product.Price - discount;

            var newStock = product.Stock - dto.Qty;
            _productsService.Patch(product.Id, newStock);

            var stockLogDto = new StockLogDTO
            {
                Productid = dto.Productid,
                Qty = dto.Qty,
                Createdat = dto.Createat
            };
            _stockLogService.Post(stockLogDto);

            var sale = SaleParser.ToEntity(dto);

            if (string.IsNullOrEmpty(sale.Code))
            {
                sale.Code = Guid.NewGuid().ToString();
            }

            _dbContext.Add(sale);
            _dbContext.SaveChanges();

            return sale;
        }
    }
}
