using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public class Moeda
    {
        public string Nome { get; set; }
        public string Simbolo { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCriacao { get; set; }

        public Moeda(string nome, string simbolo)
        {
            Nome = nome;
            Simbolo = simbolo;
            Preco = 1;
            DataCriacao = DateTime.Now;
        } 
    }
}
