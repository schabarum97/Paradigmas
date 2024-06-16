using API_TF.Services.DTO;
using API_TF.Services.Execptions;

namespace API_TF.Services.Validate
{
    public class PromotionValidate
    {
        public static bool Execute(PromotionDTO dto)
        {
            if (dto.Promotiontype != 0 && dto.Promotiontype != 1)
            {
                throw new InvalidEntityException("Tipo de promoção inválida para a gravação");
            }

            return true;

        }
    }
}
