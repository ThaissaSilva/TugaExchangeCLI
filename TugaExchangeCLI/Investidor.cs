using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TugaExchangeCLI
{
    public class Investidor
    {
        public string Nome { get; set; }
        public string? Password { get; set; }
        public decimal Saldo { get; set; } // Saldo em EUR
        public Dictionary<string, decimal> Portfolio { get; set; } = new Dictionary<string, decimal>(); // Nome da moeda (e não o seu símbolo) é a KEY e a quantidade dessa moeda na carteira é a VALUE
        public List<Transacao> TransacoesIndividuais { get; set; } = new List<Transacao>(); // Transações só do investidor em questão
        public static List<Transacao> TodasAsTransacoes { get; set; } = new List<Transacao>(); // Transações de todos os investidores (vai ser acessada pelo administrador)

        public Investidor(string nome)
        {
            Nome = nome;
        }

        public Investidor() // Só para testes
        {
            Nome = "Teste";
        }
        public void FazerDeposito(decimal quantidade)
        {
            Saldo += quantidade;
        }

        public void AdicionarAoPortfolio(string nome, decimal quantidade)
        {
            if (Portfolio.ContainsKey(nome))
            {
                Portfolio[nome] += quantidade;
            }
            else
            {
                Portfolio.Add(nome, quantidade);
            }           
        }

        public void RemoverDoPortfolio(string nome, decimal quantidadeARemover)
        {
            if (Portfolio.ContainsKey(nome))
            {
                // Verifica se a quantidade da moeda é suficiente.
                decimal quantidadeNoPortfolio = Portfolio[nome];
                if (quantidadeNoPortfolio < quantidadeARemover)
                {
                    throw new InsufficientCoinsException("Você não possui criptomoedas deste tipo suficientes.");
                }
                else
                {
                    Portfolio[nome] -= quantidadeARemover;
                }
            }
            else
            {
                throw new CoinCouldNotBeFoundException("A moeda não existe no Portfolio.");
            }
        }

        public void RemoverTudoDoPortfolio(string nome)
        {
            if (Portfolio.ContainsKey(nome))
            {
                Portfolio.Remove(nome);
            }
            else
            {
                throw new CoinCouldNotBeFoundException("A moeda não existe no Portfolio.");
            }
        }

        public void AdicionarTransacao(string tipo, DateTime data, decimal preco, decimal quantidade)
        {
            Transacao transacao = new Transacao(tipo, data, preco, quantidade);
            TodasAsTransacoes.Add(transacao);
            TransacoesIndividuais.Add(transacao);
        }

        public void ComprarMoeda(string nomeOuSimbolo, decimal quantidade)
        {
            var administrador = new Administrador();
            var moeda = administrador.ObterMoeda(nomeOuSimbolo);

            if (quantidade <= 0)
            {
                throw new QuantityIsSmallerThanOrEqualToZeroException("A quantidade que introduziu deve ser maior do que zero.");
            }
            else
            {
                var subtotal = moeda.Preco * quantidade;
                var taxa = subtotal * (decimal)0.01;
                var total = subtotal + taxa;

                if (Saldo < total)
                {
                    throw new InsufficientFundsException("O seu saldo em EUR é insuficiente para realizar esta operação");
                }
                else
                {
                    // Preciso buscar o nome da moeda graças ao
                    // seu símbolo antes de adicionar o que for 
                    // ao Portfolio.

                    if (nomeOuSimbolo == moeda.Nome)
                    {
                        AdicionarAoPortfolio(nomeOuSimbolo, quantidade);
                        Saldo -= total;
                        Administrador.SomaComissoes += taxa;
                        AdicionarTransacao("Compra",DateTime.Now,moeda.Preco,quantidade);
                    }
                    else if (nomeOuSimbolo == moeda.Simbolo)
                    {
                        AdicionarAoPortfolio(moeda.Nome, quantidade);
                        Saldo -= total;
                        Administrador.SomaComissoes += taxa;
                        AdicionarTransacao("Compra", DateTime.Now, moeda.Preco, quantidade);
                    }

                }
            }
        }

        public void VenderMoeda(string nomeOuSimbolo, [Optional] decimal? quantidade)
        {
            var administrador = new Administrador();
            var moeda = administrador.ObterMoeda(nomeOuSimbolo);

            if (quantidade <= 0)
            {
                throw new QuantityIsSmallerThanOrEqualToZeroException("A quantidade que introduziu deve ser maior do que zero.");
            }
            else if (quantidade > 0)
            {
                var subtotal = moeda.Preco * (decimal)quantidade;
                var taxa = subtotal * (decimal)0.01;

                if (nomeOuSimbolo == moeda.Nome)
                {
                    RemoverDoPortfolio(nomeOuSimbolo, (decimal)quantidade);
                    Saldo += subtotal;
                    Saldo -= taxa;
                    Administrador.SomaComissoes += taxa;
                    AdicionarTransacao("Venda",DateTime.Now, moeda.Preco,(decimal)quantidade);
                }
                else if (nomeOuSimbolo == moeda.Simbolo)
                {
                    RemoverDoPortfolio(moeda.Nome, (decimal)quantidade);
                    Saldo += subtotal;
                    Saldo -= taxa;
                    Administrador.SomaComissoes += taxa;
                    AdicionarTransacao("Venda", DateTime.Now, moeda.Preco, (decimal)quantidade);
                }
            }
            else if (quantidade == null)
            {
                decimal quantidadeDeMoedasNoPortfolio = Portfolio[moeda.Nome];
                decimal subtotal = moeda.Preco * quantidadeDeMoedasNoPortfolio;
                decimal taxa = subtotal * (decimal)0.01;

                if (nomeOuSimbolo == moeda.Nome)
                {
                    RemoverTudoDoPortfolio(nomeOuSimbolo);
                    Saldo += subtotal;
                    Saldo -= taxa;
                    Administrador.SomaComissoes += taxa;
                    AdicionarTransacao("Venda", DateTime.Now, moeda.Preco, quantidadeDeMoedasNoPortfolio);
                }
                else if (nomeOuSimbolo == moeda.Simbolo)
                {
                    RemoverTudoDoPortfolio(moeda.Nome);
                    Saldo += subtotal;
                    Saldo -= taxa;
                    Administrador.SomaComissoes += taxa;
                    AdicionarTransacao("Venda", DateTime.Now, moeda.Preco, quantidadeDeMoedasNoPortfolio);
                }
            }
        }
    }


}
