using System.Collections.Generic;
using System.Linq;
using Trabalho_T2.classes;
using Trabalho_T2.Interfaces;

namespace Trabalho_T2.Relatorios
{
    public class MaisVendidos : IRelatorio
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            return produtos.OrderByDescending(p => p.QtdVendida).Take(5).ToList();
        }
    }
}
