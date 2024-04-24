using System;
using System.Collections.Generic;
using System.Linq;
using Trabalho_T2.classes;
using Trabalho_T2.Interfaces;
/*
namespace Trabalho_T2.Relatorios
{
    public class MediaPrecoCategoria : IRelatorio
    {
        public void Imprimir(List<Produto> produtos)
        {
            var mediaPrecoPorCategoria = produtos.GroupBy(p => p.Categoria)
                                                 .Select(g => new
                                                 {
                                                     Categoria = g.Key,
                                                     PrecoMedio = g.Average(p => p.Preco)
                                                 });
            Console.WriteLine("Média de preço por categoria:");
            foreach (var item in mediaPrecoPorCategoria)
            {
                Console.WriteLine($"{item.Categoria}: {item.PrecoMedio:C}");
            }
        }
    }
}
*/