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
        public static Dictionary<string, string> ajudaAdministrador = new Dictionary<string, string>();
        public static Dictionary<string, string> ajudaInvestidor = new Dictionary<string, string>();

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
        public static Dictionary<string,string> Ajuda(string tipoUsuario) // Ainda falta povoar os dicionários.
        {
            Dictionary<string,string> ajuda = new Dictionary<string,string>();
            if (tipoUsuario == "administrador")
            {
                ajuda = ajudaAdministrador;
                return ajuda;
            }
            else if (tipoUsuario == "investidor")
            {
                ajuda = ajudaInvestidor;
                return ajuda;
            }
            return ajuda;
        }
    }
}
