// Código adaptado daqui: https://stackoverflow.com/questions/11140884/command-line-interface-inside-your-c-sharp-application

// Precisamos chamar TugaExchangeCLI porque é lá que se encontra a classe "ComandosTransversais".
using TugaExchangeCLI;

namespace Program
{
    public class Program
    {
        static Administrador administrador = new Administrador();
        static Investidor? investidorAtual;

        static void Main()
        {
            // Criar a variável que vai conter os dados inseridos pelo usuário.
            string? linhaComando = null;

            // Digitar "exit" faz com que o usuário saia da aplicação.
            // Portanto, a lógica do programa deve ser executada
            // enquanto o usuário não digitar "exit".
            while (linhaComando != "exit")
            {
                // Recolher os dados inseridos pelo usuário.
                linhaComando = Console.ReadLine();

                // Separar os dados inseridos pelo usuário graças aos espaços em branco.
                string[] comando = linhaComando.Split(' ');

                // Identificar o comando principal com base na
                // primeira string do vetor de strings "comando"
                // e verificar se o número de argumentos está correto.
                if (investidorAtual == null)
                {
                    ChamarComandosAdmin(comando);
                }
                else
                {
                    ChamarComandosInvestidor(comando);
                }
            }
        }

        private static void ChamarComandosInvestidor(string[] comando)
        {
            switch (comando[0])
            {
                case "help":
                    var ajudaInvestidor = ComandosTransversais.Ajuda("investidor");
                    foreach (KeyValuePair<string, string> kvp in ajudaInvestidor)
                    {
                        Console.WriteLine(kvp.Key, kvp.Value);
                    }
                    break;
                case "clear":
                    ComandosTransversais.Limpar();
                    break;
                case "exit":
                    ComandosTransversais.Sair();
                    break;
                case "change-password":
                    if (comando.Length == 5)
                    {
                        if (comando[1] == "-o")
                        {
                            if (comando[3] == "-n")
                            {
                                string antigaPassword = comando[2];
                                string novaPassword = comando[4];

                                try
                                {
                                    investidorAtual.MudarPassword(antigaPassword, novaPassword);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("O terceiro elemento deve ser -n.");
                                Console.WriteLine("Exemplo: change-password -o antigaPassword -n novaPassword");
                            }
                        }
                        else
                        {
                            Console.WriteLine("O segundo elemento deve ser -o.");
                            Console.WriteLine("Exemplo: change-password -o antigaPassword -n novaPassword");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: change - password - o antigaPassword - n novaPassword");
                    }
                    break;
                case "show-transactions":
                    if (comando.Length == 1)
                    {                     
                        (var tiposTransacoes, var datasTransacoes, var precosDeCompra, var quantidades, var valoresTotais) = investidorAtual.ObterTransacoes();

                        var list = tiposTransacoes.Zip(datasTransacoes, (x, y) => x + y).Zip(precosDeCompra, (x, y) => x + y).Zip(quantidades, (x, y)=> x+y).Zip(valoresTotais,(x,y)=>x+y);

                        foreach (var data in list)
                        {
                            Console.WriteLine(data);
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando show-transactions não requer argumentos.");
                    }
                    break;
                case "show-portfolio":
                    if (comando.Length == 1)
                    {
                        (var nomesMoedas, var quantidadeMoedas, var precosAtuais) = investidorAtual.MostrarPortfolio();

                        var list = nomesMoedas.Zip(quantidadeMoedas, (x, y) => x + y).Zip(precosAtuais, (x, y) => x + y);

                        foreach (var data in list)
                        {
                            Console.WriteLine(data);
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando show-transactions não requer argumentos.");
                    }
                    break;
                case "make-deposit":
                    if (comando.Length == 2)
                    {
                        string quantidadeString = comando[1];
                        decimal quantidade;
                        bool isValid = Decimal.TryParse(quantidadeString, out quantidade);

                        if (isValid)
                        {
                            investidorAtual.FazerDeposito(quantidade);
                        }
                        else
                        {
                            Console.WriteLine("Introduza uma quantidade válida da próxima vez.");
                        }
                    }
                    else if (comando.Length == 1)
                    {
                        Console.WriteLine("O comando make-deposit requer que introduza a quantia em EUR que deseja depositar.");
                    }
                    else if (comando.Length >= 3)
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: make-deposit 150");
                    }
                    break;
                case "buy-coin":
                    if (comando.Length == 3)
                    {
                        string nomeOuSimbolo = comando[1];
                        string quantidadeString = comando[2];
                        decimal quantidade;

                        bool isValid = decimal.TryParse(quantidadeString,out quantidade);

                        if (isValid)
                        {
                            try
                            {
                                investidorAtual.ComprarMoeda(nomeOuSimbolo,quantidade);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Introduza uma quantidade válida da próxima vez.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: buy-coin Bitcoin 30");
                    }
                    break;
            }
        }

        private static void ChamarComandosAdmin(string[] comando)
        {
            switch (comando[0])
            {
                case "help":
                    var ajudaAdministrador = ComandosTransversais.Ajuda("administrador");
                    foreach (KeyValuePair<string, string> kvp in ajudaAdministrador)
                    {
                        Console.WriteLine(kvp.Key, kvp.Value);
                    }
                    break;
                case "clear":
                    ComandosTransversais.Limpar();
                    break;
                case "exit":
                    ComandosTransversais.Sair();
                    break;
                case "add-coin":
                    string nome;
                    string simbolo;
                    if (comando.Length != 5)
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: add-coin -name Bitcoin -symbol BTC");
                    }
                    else // se o tamanho for 5, ainda é preciso verificar que
                         // o segundo e o quarto elementos são "-name" e "-symbol" (a começar por "-name").
                    {
                        if (comando[1] != "-name")
                        {
                            Console.WriteLine("O segundo elemento deve ser -name.");
                            Console.WriteLine("Exemplo: add-coin -name Bitcoin -symbol BTC");
                        }
                        else // ou seja, se o segundo elemento for "-name".
                        {
                            if (comando[3] == "-symbol")
                            {
                                // Isso quer dizer que o nome da moeda não contém espaços - é o que queremos.
                                nome = comando[2];
                                simbolo = comando[4];

                                try
                                {
                                    administrador.AdicionarMoeda(nome, simbolo);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Tem certeza de que introduziu o nome da moeda corretamente?");
                                Console.WriteLine("O nome da moeda não pode conter espaços.");
                            }
                        }
                    }
                    break;
                case "remove-coin":
                    if (comando.Length == 3)
                    {
                        if (comando[1] == "-name")
                        {
                            string nomeMoeda = comando[2];
                            try
                            {
                                administrador.RemoverMoeda(nomeMoeda);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("O segundo elemento deve ser -name.");
                            Console.WriteLine("Exemplo: remove-coin -name Bitcoin");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: remove-coin -name Bitcoin");
                    }
                    break;
                case "show-transactions":
                    if (comando.Length == 1)
                    {
                        (var datasTransacoes, var tiposTransacoes, var comissoesTransacoes) = administrador.ObterTransacoes();

                        var list = datasTransacoes.Zip(tiposTransacoes, (x, y) => x + y).Zip(comissoesTransacoes, (x, y) => x + y);

                        foreach (var data in list)
                        {
                            Console.WriteLine(data);
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando show-transactions não requer argumentos.");
                    }
                    break;
                case "list-coins":
                    if (comando.Length == 1)
                    {
                        // Permite obter as duas listas desejadas através do método Obter...();
                        // e as salvar em duas variáveis separadas.
                        (var nomesMoedas, var datasMoedas) = administrador.ObterNomeEDatasMoedas();

                        // O código abaixo permite imprimir os resultados de duas listas
                        // ao mesmo tempo e foi retirado de
                        // https://stackoverflow.com/questions/24277668/how-to-print-two-list-results-together.

                        foreach (var a in nomesMoedas.Zip(datasMoedas, (n, d) => new { n, d }))
                        {
                            Console.WriteLine(a.n + " " + a.d);
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando list-coins não requer argumentos.");
                    }
                    break;
                case "save":
                    break;
                case "read":
                    break;
                case "add-investor":
                    if (comando.Length == 3)
                    {
                        if (comando[1] == "-name")
                        {
                            string nomeInvestidor = comando[2];

                            try
                            {
                                administrador.AdicionarInvestidor(nomeInvestidor);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("O segundo elemento deve ser -name.");
                            Console.WriteLine("Exemplo: add-investor -name ThaissaSilva");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: add-investor -name ThaissaSilva");
                    }
                    break;
                case "login":
                    break;                
            }
        }
    }
}
