using System.Collections.Generic;
using System.Linq;
using Trabalho_T2.classes;
using Trabalho_T2.Interfaces;

namespace Trabalho_T2.Relatorios
{
    public class MaisEstoque : IRelatorio
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            return produtos.OrderByDescending(p => p.Estoque).Take(3).ToList();

        }
    }
}
