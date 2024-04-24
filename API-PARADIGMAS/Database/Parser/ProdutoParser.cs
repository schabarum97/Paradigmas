using API_PARADIGMAS.Database.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace API_PARADIGMAS.Database.Parser
{

    public enum Header
    {
        Codigo = 0,
        Descricao = 1,
        Categoria = 2,
        Preco = 3,
        Estoque = 4,
        QtdVendida = 5
    }

    public class ProdutoParser
    {
        public static List<Produto> ConveterLista(string arquivo)
        {
            List<Produto> produtos = new();

            var linhas = arquivo.Split('\n').ToList();

            linhas.Remove(linhas.First());

            foreach (var line in linhas)
            {
                Produto produto = new()
                {
                    Codigo = Convert.ToInt32(line.Split(';')[(int)Header.Codigo]),
                    Descricao = line.Split(';')[(int)Header.Descricao],
                    Categoria = line.Split(';')[(int)Header.Categoria],
                    Preco = Convert.ToDouble(line.Split(";")[(int)Header.Preco], CultureInfo.InvariantCulture),
                    Estoque = Convert.ToInt32(line.Split(';')[(int)Header.Estoque]),
                    QtdVendida = Convert.ToInt32(line.Split(';')[(int)Header.QtdVendida])
                };

                produtos.Add(produto);
            }

            return produtos;

        }
    }
}
