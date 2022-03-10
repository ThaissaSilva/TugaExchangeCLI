using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public class Investidor
    {
        public string Nome { get; set; }
        public string Password { get; set; }
        public decimal Saldo { get; set; } // Saldo em EUR
        public Dictionary<string, decimal> Portfolio { get; set; } = new Dictionary<string, decimal>();

        public void FazerDeposito(decimal quantidade)
        {
            Saldo += quantidade;
        }

        public void ComprarMoeda(string nome, decimal quantidade)
        {
            var administrador = new Administrador();
            var precoMoeda = administrador.ObterPrecoMoeda(nome);

            if (quantidade <= 0)
            {
                throw new QuantityIsSmallerThanOrEqualToZeroException("A quantidade que introduziu deve ser maior do que zero.");
            }
            else
            {
                var subtotal = precoMoeda * quantidade;
                var fee = subtotal * (decimal)0.01;
                var total = subtotal + fee;

                if (Saldo < total)
                {
                    throw new InsufficientFundsException("O seu saldo em EUR é insuficiente para realizar esta operação");
                }
                else
                {

                    // Portfolio.Add(nome, quantidade);
                    Saldo -= total;
                }
            }


        }
    }


}
