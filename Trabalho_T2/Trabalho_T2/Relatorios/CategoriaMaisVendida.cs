using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_T2.classes;
using Trabalho_T2.Interfaces;
/*
namespace Trabalho_T2.Relatorios
{
    public class CategoriaMaisVendida : IRelatorio
    {
        public void Imprimir(List<Produto> produtos)
        {
            var categoriaMaisVendida = produtos.GroupBy(p => p.Categoria)
                                               .OrderByDescending(g => g.Sum(p => p.QtdVendida))
                                               .Select(g => g.Key)
                                               .FirstOrDefault();
            Console.WriteLine($"Categoria mais vendida: {categoriaMaisVendida}");
        }
    }
}
*/
