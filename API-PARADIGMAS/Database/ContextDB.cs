using API_PARADIGMAS.Database.Models;
using API_PARADIGMAS.Database.Parser;
using System.Collections.Generic;
using System.IO;

namespace API_PARADIGMAS.Database
{
    public class ContextDB
    {
        private readonly string _dataset;
        private readonly List<Produto> _produtos;

        public ContextDB() 
        {
            _dataset = File.ReadAllText("Dataset.csv");
            _produtos = ProdutoParser.ConveterLista(_dataset);
        }
        public List<Produto> Produto => _produtos;
    }
}
