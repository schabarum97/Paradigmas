using System;

public class Programa
{
    public static void Main(string[] args)
    {
        int A, B, C;

        try
        {
            A = EntradaDados("Digite o valor de A: ");
            B = EntradaDados("Digite o valor de B: ");
            C = EntradaDados("Digite o valor de C: ");

            int R = Calculo(A, B);
            int S = Calculo(B, C);
            int D = (R + S) / 2;

            Console.WriteLine($"O resultado da expressão D = R + S² é: {D}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Você digitou um valor inválido. Tente novamente.");
        }
        catch (Exception err)
        {
            Console.WriteLine($"Ocorreu um erro: {err.Message}");
        }
    }

    public static int EntradaDados(string mensagem)
    {
        int ret;
        bool CaracValido = false;

        do
        {
            Console.Write(mensagem);
            string i = Console.ReadLine();
            CaracValido = int.TryParse(i, out ret);

            if (!CaracValido || ret <= 0)
            {
                Console.WriteLine("Por favor, digite um número inteiro e positivo.");
                CaracValido = false;
            }
        } while (!CaracValido);

        return ret;
    }

    public static int Calculo(int A, int B)
    {
        return (A + B) * (A + B);
    }
}
