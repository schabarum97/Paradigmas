using System;
using System.Collections.Generic;
using System.Linq;
using Trabalho_T2.classes;
using Trabalho_T2.Interfaces;

namespace Trabalho_T2.Relatorios
{
    public class ExcessoEstoque : IRelatorio
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            return produtos.Where(p => p.Estoque >= p.QtdVendida * 3).ToList();
            
        }
    }
}
