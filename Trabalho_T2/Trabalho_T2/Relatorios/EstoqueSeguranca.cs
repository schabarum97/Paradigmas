using System.Collections.Generic;
using System.Linq;
using Trabalho_T2.classes;

namespace Trabalho_T2.Relatorios
{
    public class EstoqueSeguranca
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            return produtos.Where(p => p.Estoque < p.QtdVendida * 0.33).ToList();
        }
    }
}
