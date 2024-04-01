using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trabalho_T2.classes;

class Program
{
    public static void Main(string[] args)
    {
        var DataSet = File.ReadAllText("..\\..\\..\\Dataset.csv");
        var list = ProdutoParser.ConveterLista(DataSet);
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1) Imprimir relatório de produtos mais vendidos (Top 5)");
            Console.WriteLine("2) Imprimir relatório de produtos com mais estoque (Top 3)");
            Console.WriteLine("3) Imprimir relatório da categoria mais vendida (Top 1)");
            Console.WriteLine("4) Imprimir relatório de produtos menos vendidos (Top 5)");
            Console.WriteLine("5) Imprimir relatório de estoque de segurança");
            Console.WriteLine("6) Imprimir relatório de excesso de estoque");
            Console.WriteLine("7) Imprimir média de preço por categoria");
            Console.WriteLine("8) Sair");
            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        ImprimirProdutosMaisVendidos(list);
                        break;
                    case 2:
                        ImprimirProdutosComMaisEstoque(list);
                        break;
                    case 3:
                        ImprimirCategoriaMaisVendida(list);
                        break;
                    case 4:
                        ImprimirProdutosMenosVendidos(list);
                        break;
                    case 5:
                        ImprimirEstoqueSeguranca(list);
                        break;
                    case 6:
                        ImprimirExcessoEstoque(list);
                        break;
                    case 7:
                        ImprimirMediaPrecoPorCategoria(list);
                        break;
                    case 8:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            }

            static void ImprimirProdutosMaisVendidos(List<Produto> produtos)
            {
                var top5 = produtos.OrderByDescending(p => p.QtdVendida).Take(5);
                Console.WriteLine("Produtos mais vendidos (Top 5):");
                foreach (var produto in top5)
                {
                    Console.WriteLine($"{produto.Codigo} - {produto.Descricao}");
                }
            }

            static void ImprimirProdutosComMaisEstoque(List<Produto> produtos)
            {
                var top3 = produtos.OrderByDescending(p => p.Estoque).Take(3);
                Console.WriteLine("Produtos com mais estoque (Top 3):");
                foreach (var produto in top3)
                {
                    Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.Estoque}");
                }
            }

            static void ImprimirCategoriaMaisVendida(List<Produto> produtos)
            {
                var categoriaMaisVendida = produtos.GroupBy(p => p.Categoria)
                                                   .OrderByDescending(g => g.Sum(p => p.QtdVendida))
                                                   .Select(g => g.Key)
                                                   .FirstOrDefault();
                Console.WriteLine($"Categoria mais vendida: {categoriaMaisVendida}");
            }

            static void ImprimirProdutosMenosVendidos(List<Produto> produtos)
            {
                var menosVendida = produtos.OrderBy(p => p.QtdVendida).Take(5);
                Console.WriteLine("Produtos menos vendidos (Top 5):");
                foreach (var produto in menosVendida)
                {
                    Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.QtdVendida}");
                }
            }

            static void ImprimirEstoqueSeguranca(List<Produto> produtos)
            {
                var estoqueSeguranca = produtos.Where(p => p.Estoque < p.QtdVendida * 0.33);
                Console.WriteLine("Produtos com estoque de segurança:");
                foreach (var produto in estoqueSeguranca)
                {
                    Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.Estoque}");
                }
            }

            static void ImprimirExcessoEstoque(List<Produto> produtos)
            {
                var excessoEstoque = produtos.Where(p => p.Estoque >= p.QtdVendida * 3);
                Console.WriteLine("Produtos com excesso de estoque:");
                foreach (var produto in excessoEstoque)
                {
                    Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.Estoque}");
                }
            }

            static void ImprimirMediaPrecoPorCategoria(List<Produto> produtos)
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
}
