using System;

namespace API_TF.Services.Execptions
{
    public class InsufficientStockException: Exception
    {
        public InsufficientStockException(string message) : base(message) { }
    }
}
