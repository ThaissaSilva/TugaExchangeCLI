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
        public List<Moeda> Moedas { get; set; } = new List<Moeda>();

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
                throw new CoinNameAlreadyExistsException("O nome que inseriu já existe.");
            }
            else if (moedaPorSimbolo != null)
            {
                throw new CoinSymbolAlreadyExistsException("O símbolo que inseriu já existe.");
            }
            else
            {
                throw new CoinAlreadyExistsException("A nome e o símbolo que inseriu já existem.");
            }
        }
    }
}
