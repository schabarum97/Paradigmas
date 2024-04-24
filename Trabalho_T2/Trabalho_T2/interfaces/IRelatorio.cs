using System.Collections.Generic;
using Trabalho_T2.classes;

namespace Trabalho_T2.Interfaces
{
    public interface IRelatorio
    {
        List<Produto> Imprimir(List<Produto> produtos);
    }
}