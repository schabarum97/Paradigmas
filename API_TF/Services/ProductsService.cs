using API_TF.DataBase;
using API_TF.DataBase.Models;
using System.Linq;

namespace API_TF.Services
{
    public class ProductsService
    {
        private readonly TfDbContext _dbContext;
        public ProductsService(TfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TbProduct GetByBarcode(string barcode)
        {
            var existingEntity = _dbContext.TbProducts
                                 .Where(c => c.Barcode == barcode)
                                 .FirstOrDefault();
            return existingEntity;
        }
    }
}
