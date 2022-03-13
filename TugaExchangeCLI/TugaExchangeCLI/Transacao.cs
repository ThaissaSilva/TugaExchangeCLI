using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public class Transacao
    {
        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Comissao { get; set; }
        public decimal ValorTotal { get; set; } // Valor total da transação, considerando a comissão

        public Transacao(DateTime data, decimal preco, decimal quantidade)
        {
            Data = data;
            Preco = preco;
            Quantidade = quantidade;
            Subtotal = preco * quantidade;
            Comissao = Subtotal * (decimal)0.01;
        }
    }

    public class Compra : Transacao
    {
        public string NomeMoeda { get; set; }

        public Compra(string nomeMoeda, DateTime data, decimal preco, decimal quantidade) : base(data, preco, quantidade)
        {
            NomeMoeda = nomeMoeda;
            ValorTotal = Subtotal + Comissao;
        }
    }

    public class Venda : Transacao
    {
        public Venda(DateTime data, decimal preco, decimal quantidade) : base(data, preco, quantidade)
        {
            ValorTotal = Subtotal - Comissao;
        }
    }
}
