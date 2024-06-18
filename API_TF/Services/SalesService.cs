﻿using API_TF.DataBase;
using API_TF.DataBase.Models;
using API_TF.Services.DTO;
using API_TF.Services.Execptions;
using API_TF.Services.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_TF.Services
{
    public class SalesService
    {
        private readonly TfDbContext _dbContext;
        private readonly ProductsService _productsService;
        private readonly StockLogService _stockLogService;
        private readonly PromotionsService _promotionService;

        public SalesService(TfDbContext dbContext, ProductsService productsService, StockLogService stockLogService, PromotionsService promotionService)
        {
            _dbContext = dbContext;
            _productsService = productsService;
            _stockLogService = stockLogService;
            _promotionService = promotionService;
        }

        public IEnumerable<TbSale> GetById(string code)
        { 
            var existingentity = _dbContext.TbSales
                                           .ToList()
                                           .Where(s => s.Code == code);

            return existingentity;
        }

        public IEnumerable<TbSale> Post(List<SaleDTO> dtoList)
        {
            var sales = new List<TbSale>();
            var currentTime = DateTime.Now;
            var code = Guid.NewGuid().ToString();

            foreach (var dto in dtoList)
            {
                var product = _productsService.GetById(dto.Productid);
                if (product == null)
                    throw new NotFoundException("Produto não existe");

                if (product.Stock < dto.Qty)
                    throw new InsufficientStockException("Estoque insuficiente para a movimentação");

                var promotions = _promotionService.GetActivePromotions(dto.Productid);

                decimal unitPrice = product.Price;
                decimal discount = 0;
                decimal Tdiscount = 0;
                foreach (var promotion in promotions)
                {
                    discount = ApplyPromotion(unitPrice, promotion);
                    Tdiscount += discount;
                }

                var newStock = product.Stock - dto.Qty;
                _productsService.Patch(product.Id, newStock);

                var stockLogDto = new StockLogDTO
                {
                    Productid = dto.Productid,
                    Qty = -dto.Qty,
                    Createdat = DateTime.Now
                };
                _stockLogService.Post(stockLogDto);

                var sale = SaleParser.ToEntity(dto);

                sale.Code = code;
                sale.Price = product.Price;
                sale.Discount = Tdiscount;
                sale.Createat = currentTime;

                _dbContext.Add(sale);
                sales.Add(sale);
            }

            _dbContext.SaveChanges();

            return sales;
        }

        public decimal ApplyPromotion(decimal price, TbPromotion promotion)
        {
            decimal discountedPrice;

            switch (promotion.Promotiontype)
            {
                case 0:
                    discountedPrice = price * (1 - promotion.Value / 100);
                    break;
                case 1:
                    discountedPrice = price - promotion.Value;
                    break;
                default:
                    discountedPrice = price;
                    break;
            }

            return price - discountedPrice;
        }

        public List<SalesReportDTO> GetSalesReportByPeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate == default || endDate == default)
            {
                throw new BadRequestException("As datas de início e fim são obrigatórias.");
            }

            var query = from sale in _dbContext.TbSales
                        join product in _dbContext.TbProducts on sale.Productid equals product.Id
                        where sale.Createat >= startDate && sale.Createat < endDate.AddDays(1)
                        group new { sale, product } by new { sale.Code, sale.Createat } into saleGroup
                        select new SalesReportDTO
                        {
                            SaleCode = saleGroup.Key.Code,
                            SaleDate = saleGroup.Key.Createat,
                            Products = saleGroup.Select(g => new SaleProductDTO
                            {
                                ProductDescription = g.product.Description,
                                Price = g.sale.Price,
                                Quantity = g.sale.Qty
                            }).ToList()
                        };

            var result = query.ToList();

            if (!result.Any())
            {
                throw new NotFoundException("Nenhuma venda encontrada para o período.");
            }

            return result;
        }
    }
}
