using System;

namespace ApiWebDB.Services.DTOs
{
    public class ClienteDTO
    {
        /// <example>
        /// Renan Schabarum
        /// </example>
        public string Nome { get; set; }
        /// <example>
        /// 2002-04-22
        /// </example>
        public DateOnly? Nascimento { get; set; }
        /// <example>
        /// 49988580657
        /// </example>
        public string Telefone { get; set; }
        /// <example>
        /// 11173766995
        /// </example>
        public string Documento { get; set; }
        /// <summary>
        /// 0 - CPF 1 - CNPJ 2 - Passaporte 3 - CNH 99 - Outros
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int Tipodoc { get; set; }
    }
}
