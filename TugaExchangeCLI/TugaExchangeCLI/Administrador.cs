using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public class Administrador
    {
        // Uma característica (ou propriedade) do administrador é que
        // ele pode acessar e modificar uma lista de moedas.
        // As listas são estáticas porque quero que todos os administradores possam
        // acessar os mesmos dados.
        public static List<Moeda> Moedas { get; set; } = new List<Moeda>();
        public static decimal SomaComissoes { get; set; }

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

        
    }
}
