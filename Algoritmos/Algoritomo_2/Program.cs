using System;
using System.Collections.Generic;
using System.Linq;


Console.WriteLine("Informe a quantidade de números a serem lidos:");
int quant;

while (!int.TryParse(Console.ReadLine(), out quant) || quant <= 0)
{
    Console.WriteLine("Digite um número inteiro positivo:");
}

var numeros = new List<int>();

for (int i = 0; i < quant; i++)
{
    Console.WriteLine($"Informe o número {i + 1}:");
    int numero;

    while (!int.TryParse(Console.ReadLine(), out numero) || numero <= 0)
    {
        Console.WriteLine("Digite um número válido:");
    }

    numeros.Add(numero);
}

if (numeros.Count > 0)
{
    Console.WriteLine($"O maior número é: {numeros.Max()}");
    Console.WriteLine($"O menor número é: {numeros.Min()}");
    Console.WriteLine($"A média dos números é: {numeros.Average()}");
}

