using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TugaExchangeCLI
{
    public static class ComandosTransversais
    // A classe ComandosTransversais é estática porque
    // queremos poder acessar os seus métodos sem termos
    // de criar um objeto específico desta classe.
    {             
        // Define o comportamento da função Clear().
        public static void Limpar()
        {
            Console.Clear();
        }

        // Define o comportamento da função Exit().
        public static void Sair()
        {
            Environment.Exit(0);
        }

        // Define o comportamento da função Help().
        // A função Help() fornece ajuda contextual:
        // se comporta de maneira diferente, conforme o utilizador
        // for um administrator ou um investidor.
        
    }
}
