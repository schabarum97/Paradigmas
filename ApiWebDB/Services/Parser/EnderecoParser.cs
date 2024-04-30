using ApiWebDB.BaseDados.Models;
using ApiWebDB.Services.DTOs;

namespace ApiWebDB.Services.Parser
{
    public static class EnderecoParser
    {
        public static TbEndereco ToEntity(EnderecoDTO dto)
        {
            return new TbEndereco
            {
                Cep = dto.Cep,
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,    
                Complemento = dto.Complemento,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Uf = dto.Uf,
                Clienteid = dto.Clienteid,
                Status = dto.Status
            };
        }
    }
}
