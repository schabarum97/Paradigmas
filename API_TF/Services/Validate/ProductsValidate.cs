using API_TF.Services.DTO;
using API_TF.Services.Execptions;
using Microsoft.AspNetCore.Routing.Matching;
using System.Data;

namespace API_TF.Services.Validate
{
    public class ProductsValidate
    {
        public static void ValidateEAN13(string barcode)
        {
            if (barcode.Length != 13)
                throw new BadRequestException("Para o tipo de barcode EAN13 deve ter 13 caracteres");
        }
        public static void ValidateDUN14(string barcode)
        {
            if (barcode.Length != 14)
                throw new BadRequestException("Para o tipo de barcode DUN14 deve ter 14 caracteres");
        }
        public static void ValidateUPC(string barcode)
        {
            if (barcode.Length != 12)
                throw new BadRequestException("Para o tipo de barcode UPC deve ter 12 caracteres");
        }
        public static void ValidateCode11(string barcode)
        {
            foreach (char c in barcode)
            {
                if (!(char.IsDigit(c) || c == '-' || c == '*'))
                    throw new BadRequestException("Para o tipo de barcode Code11 só poderá ter números de 0 a 9, – e *");
            }
        }
        public static void ValidateCode39(string barcode)
        {
            foreach (char c in barcode)
            {
                if (!(char.IsLetterOrDigit(c) || c == '-' || c == '.' || c == '$' || c == '/' || c == '+' || c == '%' || c == ' '))
                    throw new BadRequestException("Para o tipo de barcode Code11 só poderá ter Letras (A a Z), numéros (0 a 9) e (-, ., $, /, +, %, e espaço)"); ;
            }
        }
        
        private static void ValidateStringLength(ProductDTO dto)
        {
            if (dto.Description.Length > 255)
               throw new BadRequestException("Tamanho da descrição não pode exceder 255 caracteres");
        }

        public static bool Execute(ProductDTO dto)
        {

            if (dto.Barcodetype.ToUpper() == "EAN-13")
                ValidateEAN13(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "DUN-14")
                ValidateDUN14(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "UPC")
                ValidateDUN14(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "CODE 11")
                ValidateCode11(dto.Barcode);
            else if (dto.Barcodetype.ToUpper() == "CODE 39")
                ValidateCode39(dto.Barcode);
            else
                throw new InvalidEntityException("Tipo de barcode inválido para a gravação");

            
            
            return true;
        }
    }
}
