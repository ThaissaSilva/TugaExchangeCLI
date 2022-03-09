using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public class Moeda
    {
        public string Nome { get; }
        public string Simbolo { get; }
        public DateTime DataCriacao { get; }

        public Moeda(string nome, string simbolo)
        {
            Nome = nome;
            Simbolo = simbolo;
            DataCriacao = DateTime.Now;
        } 
    }
}
