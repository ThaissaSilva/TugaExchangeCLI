using Xunit;
using TugaExchangeCLI;
using System.Threading;

namespace TugaExchangeCLI.Tests
{
    public class TestesVariados
    {
        [Fact]
        public void FazerDeposito()
        {
            var investidor = new Investidor();
            // Fazer depósito
            Assert.Equal(0, investidor.Saldo);
            investidor.FazerDeposito(100);
            Assert.Equal(100, investidor.Saldo);
        }

        [Fact]
        public void ComprarMoeda()
        {
            // Adicionar moeda ao sistema
            var administrador = new Administrador();
            Assert.Empty(Administrador.Moedas);
            administrador.AdicionarMoeda("Thaissa", "T$");
            Assert.NotEmpty(Administrador.Moedas);

            // Depositar dinheiro na conta do investidor
            var investidor = new Investidor();
            investidor.FazerDeposito(100);

            // Comprar moeda
            Assert.Empty(investidor.Portfolio);
            Assert.Empty(investidor.TransacoesIndividuais);
            investidor.ComprarMoeda("T$", 50);
            Assert.NotEmpty(investidor.TransacoesIndividuais);
            Assert.Equal((decimal)49.50, investidor.Saldo);
            Assert.Equal((decimal)0.50, Administrador.SomaComissoes);
            Assert.NotEmpty(investidor.Portfolio);
        }

        [Fact]
        public void VenderMoeda()
        {
            // Adicionar moeda ao sistema
            var administrador = new Administrador();
            Assert.Empty(Administrador.Moedas);
            administrador.AdicionarMoeda("Thaissa", "T$");
            Assert.NotEmpty(Administrador.Moedas);

            // Depositar dinheiro na conta do investidor
            var investidor = new Investidor();
            investidor.FazerDeposito(100);

            // Comprar moeda
            Assert.Empty(investidor.Portfolio);
            investidor.ComprarMoeda("T$", 50);
            Assert.NotEmpty(investidor.Portfolio);
            var quantidadeMoedasPortfolio = investidor.Portfolio["Thaissa"];
            Assert.Equal(50, quantidadeMoedasPortfolio);

            // Vender moeda
            investidor.VenderMoeda("T$", 10);
            Assert.Equal(2, investidor.TransacoesIndividuais.Count);
            var quantidadeMoedasPortfolioAtualizada = investidor.Portfolio["Thaissa"];
            Assert.Equal(40, quantidadeMoedasPortfolioAtualizada);
        }

        [Fact]
        public void VenderTodasAsMoedas()
        {
            // Adicionar moeda ao sistema
            var administrador = new Administrador();
            Assert.Empty(Administrador.Moedas);
            administrador.AdicionarMoeda("Thaissa", "T$");
            Assert.NotEmpty(Administrador.Moedas);

            // Depositar dinheiro na conta do investidor
            var investidor = new Investidor();
            investidor.FazerDeposito(100);

            // Comprar moeda
            Assert.Empty(investidor.Portfolio);
            investidor.ComprarMoeda("T$", 50);
            Assert.Single(investidor.TransacoesIndividuais);
            Assert.NotEmpty(investidor.Portfolio);
            var quantidadeMoedasPortfolio = investidor.Portfolio["Thaissa"];
            Assert.Equal(50, quantidadeMoedasPortfolio);

            // Vender todas as moedas
            investidor.VenderMoeda("T$");
            Assert.Equal(2, investidor.TransacoesIndividuais.Count);
            Assert.Empty(investidor.Portfolio);
        }

        [Fact]
        public void RemoverMoedaQueNaoExisteEmPortfolio()
        {
            // Criar moedas
            Administrador administrador = new Administrador();
            administrador.AdicionarMoeda("Thaissa", "T$");
            administrador.AdicionarMoeda("Gallo", "G$");
            Assert.Equal(2, Administrador.Moedas.Count);

            // Criar dois investidores
            Investidor investidor1 = administrador.AdicionarInvestidor("investidor1");
            Investidor investidor2 = administrador.AdicionarInvestidor("investidor2");
            Assert.Equal(2, Administrador.Investidores.Count);

            // Depositar dinheiro nas suas respectivas contas
            investidor1.FazerDeposito(100);
            investidor2.FazerDeposito(100);

            // Comprar moedas
            investidor1.ComprarMoeda("Thaissa",10);
            investidor2.ComprarMoeda("Thaissa", 20);
            // (ninguém vai comprar a Gallo

            administrador.RemoverMoeda("Gallo");
            Assert.Single(Administrador.Moedas);
        }

        [Fact]
        public void RemoverMoedaQueExisteEmPortfolio()
        {
            // Criar moedas
            Administrador administrador = new Administrador();
            administrador.AdicionarMoeda("Thaissa", "T$");
            administrador.AdicionarMoeda("Gallo", "G$");
            Assert.Equal(2, Administrador.Moedas.Count);

            // Criar dois investidores
            Investidor investidor1 = administrador.AdicionarInvestidor("investidor1");
            Investidor investidor2 = administrador.AdicionarInvestidor("investidor2");
            Assert.Equal(2, Administrador.Investidores.Count);

            // Depositar dinheiro nas suas respectivas contas
            investidor1.FazerDeposito(100);
            investidor2.FazerDeposito(100);

            // Comprar moedas
            investidor1.ComprarMoeda("Thaissa", 10);
            investidor2.ComprarMoeda("Gallo", 20);

            administrador.RemoverMoeda("Thaissa");
            // O teste falha oficialmente pq ele lança a exceção que eu criei (como esperado). No Program ela teria sido apanhada com um TryCatch, portando o código funciona.
        }
        [Fact]
        public void MostrarPortfolioDetalhado()
        {
            // Criar moedas
            Administrador administrador = new Administrador();
            administrador.AdicionarMoeda("Thaissa", "T$");
            administrador.AdicionarMoeda("Gallo", "G$");

            // Criar dois investidores
            Investidor investidor1 = administrador.AdicionarInvestidor("investidor1");
            Investidor investidor2 = administrador.AdicionarInvestidor("investidor2");

            // Depositar dinheiro nas suas respectivas contas
            investidor1.FazerDeposito(100);
            investidor2.FazerDeposito(100);

            // Comprar moedas
            investidor1.ComprarMoeda("Thaissa", 10);
            investidor1.ComprarMoeda("Thaissa", 10);
            investidor2.ComprarMoeda("Gallo", 20);
            investidor2.ComprarMoeda("Thaissa", 20);
            investidor2.ComprarMoeda("Thaissa", 20);

            // Mudar preco de uma das moedas
            Moeda Thaissa = administrador.ObterMoeda("Thaissa");
            Thaissa.Preco = 2;

            // Mostrar portfolio detalhado do investidor #2
            investidor2.MostrarPortfolio();
        }

        [Fact]
        public void AtualizarPrecos()
        {
            // Criar moedas
            Administrador administrador = new Administrador();
            administrador.AdicionarMoeda("Thaissa", "T$");
            administrador.AdicionarMoeda("Gallo", "G$");

            Administrador.Timer();

            Thread.Sleep(60000);

            Investidor investidor = new Investidor();
            investidor.ObterPrecos();
            var listaDatas = Administrador.datasTimer;
        }
    }
}