using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public class Transacao
    {
        public string Tipo { get; set; } // Compra ou venda
        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorTotal { get; set; } // Sem Taxa

        public Transacao(string tipo, DateTime data, decimal preco, decimal quantidade)
        {
            Tipo = tipo;
            Data = data;
            Preco = preco;
            Quantidade = quantidade;
            ValorTotal = quantidade * preco;
        }
    }
}
