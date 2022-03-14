using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TugaExchangeCLI
{
    public class Administrador
    {
        // Uma característica (ou propriedade) do administrador é que
        // ele pode acessar e modificar uma lista de moedas.
        // As listas são estáticas porque quero que todos os administradores possam acessar os mesmos dados.
        public static List<Moeda> Moedas { get; set; } = new List<Moeda>();
        public static decimal SomaComissoes { get; set; }
        public static List<Investidor> Investidores { get; set;} = new List<Investidor>();
        static System.Timers.Timer _timer;
        public static List<DateTime> datasTimer = new List<DateTime>();

        // Define o comportamento da função AdicionarMoeda([...]).
        public void AdicionarMoeda(string nome, string simbolo)
        {
            // Antes de criar uma moeda, é necessário verificar
            // que o nome da moeda e o símbolo já não constam
            // na nossa lista de moedas.
            Moeda? moedaPorNome = Moedas.Find(moedaPorNome => moedaPorNome.Nome == nome);
            Moeda? moedaPorSimbolo = Moedas.Find(moedaPorSimbolo => moedaPorSimbolo.Simbolo == simbolo);

            if (moedaPorNome == null && moedaPorSimbolo == null)
            {
                Moeda moeda = new Moeda(nome, simbolo);
                Moedas.Add(moeda);
            }
            else if (moedaPorNome != null)
            {
                if (moedaPorSimbolo != null)
                {
                    throw new CoinAlreadyExistsException("O nome e o símbolo que inseriu já existem.");
                }
                else
                {
                    throw new CoinNameAlreadyExistsException("O nome que inseriu já existe.");
                }
            }
            else if (moedaPorSimbolo != null)
            {
                throw new CoinSymbolAlreadyExistsException("O símbolo que inseriu já existe.");
            }
        }

        public (List<string>, List<DateTime>) ObterNomeEDatasMoedas()
        {
            // Cria a lista de nomes de moedas e a lista de datas de criação das moedas
            List<string> nomesMoedas = new List<string>();
            List<DateTime> datasMoedas = new List<DateTime>();

            // Povoa as listas criadas acima com os dados já disponíveis na lista de moedas
            foreach (Moeda moeda in Moedas)
            {
                nomesMoedas.Add(moeda.Nome);
                datasMoedas.Add(moeda.DataCriacao);
            }

            // Devolve as novas listas já preenchidas
            return (nomesMoedas, datasMoedas);
        }

        public Moeda ObterMoeda(string nomeOuSimbolo)
        {
            Moeda? moedaPorNome = Moedas.Find(moedaPorNome => moedaPorNome.Nome == nomeOuSimbolo);
            Moeda? moedaPorSimbolo = Moedas.Find(moedaPorSimbolo => moedaPorSimbolo.Simbolo == nomeOuSimbolo);
            Moeda? moeda = new Moeda();

            if (moedaPorNome == null)
            {
                if (moedaPorSimbolo == null)
                {
                    throw new CoinCouldNotBeFoundException("A moeda não foi encontrada.");
                }
                else
                {
                    moeda = moedaPorSimbolo;
                }               
            }
            else
            {
                moeda = moedaPorNome;
            }
            return moeda;
        }

        public void RemoverMoeda(string nome)
        {
            Moeda? moeda = Moedas.Find(moeda => moeda.Nome == nome);

            if (moeda == null)
            {
                throw new CoinCouldNotBeFoundException("A moeda não foi encontrada.");
            }
            else // a moeda foi encontrada.
            {
                // Verificar se algum investidor a tem no seu Portfolio.
                List<bool> moedaExisteEmPortfolios = new List<bool>();

                foreach (Investidor investidor in Investidores)
                {
                    bool moedaExiste = investidor.Portfolio.ContainsKey(nome);
                    moedaExisteEmPortfolios.Add(moedaExiste);
                }

                if (moedaExisteEmPortfolios.Contains(true))
                {
                    throw new CoinCannotBeRemovedException("A moeda não pode ser removida porque algum investidor a possui em seu portfolio.");
                }
                else
                {
                    Moedas.Remove(moeda);
                }
            }
        }
        
        public Investidor AdicionarInvestidor(string nome)
        {
            // Não vou deixar criar um investidor com o mesmo nome de um investidor que já existe.
            Investidor? investidorPorNome = Investidores.Find(investidorPorNome => investidorPorNome.Nome == nome);
            if (investidorPorNome == null)
            {
                Investidor investidor = new Investidor(nome);
                Investidores.Add(investidor);
                return investidor;
            }
            else
            {
                throw new InvestorAlreadyExistsException("Já existe um investidor com este nome.");
            }
        }

        public Investidor ObterInvestidor(string nome)
        {
            Investidor? investidorPorNome = Investidores.Find(investidorPorNome => investidorPorNome.Nome == nome);

            if (investidorPorNome == null)
            {
                throw new InvestorCouldNotBeFoundException("O investidor não foi encontrado.");
            }
            else
            {
                return investidorPorNome;
            }
        }

        public bool VerificarSeExistePassword(string nomeUsuario)
        {
            Investidor investidor = ObterInvestidor(nomeUsuario);

            bool passwordExiste;
            if (investidor == null) // ou seja, não existe.
            {
                throw new InvestorCouldNotBeFoundException("O investidor não foi encontrado.");
            }
            else
            {
                if (investidor.Password == null)
                {
                    passwordExiste = false;
                }
                else
                {
                    passwordExiste = true;
                }
            }
            return passwordExiste;
        }

        public Investidor FazerLogin(string nomeUsuario, string password)
        {
            // Buscar o investidor com base em seu nome:
            Administrador administrador = new Administrador();
            Investidor investidor = administrador.ObterInvestidor(nomeUsuario);

            // Verificar se a senha introduzida corresponde àquela do investidor em questão:
            if (investidor.Password == password)
            {
                return investidor;
            }
            else
            {
                throw new WrongPasswordException("A password introduzida está incorreta.");
            }
        }

        public static double ObterTaxaVariacaoAleatoria()
        {
            var random = new Random();
            return random.NextDouble() * (0.01 - (-0.01)) + (-0.01);
        }
        
        public static void Timer()
        {
            int seconds = 10 * 1000;
            var timer = new System.Timers.Timer(seconds);
            timer.Elapsed += AtualizarPrecos;
            timer.Enabled = true;
            _timer = timer;
        }

        public static void AtualizarPrecos(object? sender, ElapsedEventArgs e)
        {
            var variacao = ObterTaxaVariacaoAleatoria();

            foreach (Moeda moeda in Moedas)
            {
                moeda.AtualizarPreco(variacao);
                datasTimer.Add(DateTime.Now); // excluir esta linha depois de testar.
            }
        }

        public (List<DateTime>, List<string>,List<decimal>) ObterTransacoes()
        {
            List<string> tiposTransacoes = new List<string>();
            List<DateTime> datasTransacoes = new List<DateTime>();
            List<decimal> comissoesTransacoes = new List<decimal>();

            var todasAsTransacoes = Investidor.TodasAsTransacoes;
            foreach (Transacao transacao in todasAsTransacoes)
            {
                datasTransacoes.Add(transacao.Data);
                comissoesTransacoes.Add(transacao.Comissao);

                if (transacao is Compra)
                {
                    tiposTransacoes.Add("Compra");
                }
                else if (transacao is Venda)
                {
                    tiposTransacoes.Add("Venda");
                }
            }

            return (datasTransacoes,tiposTransacoes,comissoesTransacoes);
        }
    }
}
