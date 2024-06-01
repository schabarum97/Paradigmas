namespace API_TF.Services.DTO
{
    // Exemplo de entrada para ProductDTO:
    // {
    //     "Description": "Nome do produto",
    //     "Barcode": "1234567890123",
    //     "Barcodetype": "EAN13",
    //     "Price": 19.99,
    //     "Costprice": 15.99,
    //     "Stock": 100
    // }

    // Exemplo de saída para TbProduct:
    // {
    //     "Id": 1,
    //     "Description": "Nome do produto",
    //     "Barcode": "1234567890123",
    //     "Barcodetype": "EAN13",
    //     "Price": 19.99,
    //     "Costprice": 15.99,
    //     "Stock": 100
    // }

    public class ProductDTO
    {
        //public int Id { get; set; }
        public string Description { get; set; }
        public string Barcode {  get; set; }
        public string Barcodetype { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal Costprice { get; set; }
    }
}
