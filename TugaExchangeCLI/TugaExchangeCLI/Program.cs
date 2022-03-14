using TugaExchangeCLI;

namespace Program
{
    public class Program
    {
        static Administrador administrador = new Administrador();
        static Investidor? investidorAtual;

        static void Main()
        {
            Administrador.Timer();
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
                    if (comando.Length == 1)
                    {
                        string[] changePassword = new string[] { "change-password -o {password antiga} -n {nova password}", "Permite alterar a password do investidor." };
                        string[] showPortfolio = new string[] { "show-portfolio", "Mostra o portfolio do investidor." };
                        string[] showTransactions = new string[] { "show-transactions", "Mostra todas as transações efetuadas pelo investidor, a data, o preço de compra, a quantidade, e o valor total da transação." };
                        string[] makeDeposit = new string[] { "make-deposit {quantidade}", "Permite fazer um depósito em EUR." };
                        string[] buyCoin = new string[] { "buy-coin {nome da moeda} {quantidade}", "Permite fazer uma transação de compra. Pode ser inserido tanto o nome da moeda como o seu símbolo, seguido da quantidade desejada." };
                        string[] sellCoin = new string[] { "sell-coin {nome da moeda} {quantidade}", "Permite fazer uma transação de venda. Pode ser inserido tanto o nome da moeda como o seu símbolo, seguido da quantidade desejada. A quantidade é opcional. Se for omitida, então é utilizado o valor total em carteira." };
                        string[] showPrices = new string[] { "show-prices", "Mostrar as cotações atuais de todas as moedas disponíveis na corretora." };

                        var ajudaInvestidor = new Dictionary<string, string>
                        {
                            {changePassword[0],changePassword[1] },
                            {showPortfolio[0],showPortfolio[1] },
                            {showTransactions[0], showTransactions[1] },
                            {makeDeposit[0], makeDeposit[1] },
                            {buyCoin[0],buyCoin[1] },
                            {sellCoin[0], sellCoin[1] },
                            {showPrices[0], showPrices[1] }
                        };

                        foreach (KeyValuePair<string, string> kvp in ajudaInvestidor)
                        {
                            Console.WriteLine($"{kvp.Key} => {kvp.Value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando help não requer argumentos.");
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

                        var list = tiposTransacoes.Zip(datasTransacoes, (x, y) => x + " " + y).Zip(precosDeCompra, (x, y) => x + " " + y).Zip(quantidades, (x, y) => x + " " + y).Zip(valoresTotais, (x, y) => x + " " + y);

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

                        var list = nomesMoedas.Zip(quantidadeMoedas, (x, y) => x + " " + y).Zip(precosAtuais, (x, y) => x + " " + y);

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

                        bool isValid = decimal.TryParse(quantidadeString, out quantidade);

                        if (isValid)
                        {
                            try
                            {
                                investidorAtual.ComprarMoeda(nomeOuSimbolo, quantidade);
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
                case "sell-coin":
                    string nomeMoeda;
                    if (comando.Length == 2)
                    {
                        nomeMoeda = comando[1];

                        try
                        {
                            investidorAtual.VenderMoeda(nomeMoeda);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else if (comando.Length == 3)
                    {
                        nomeMoeda = comando[1];
                        string quantidadeString = comando[2];
                        decimal quantidade;

                        bool isValid = decimal.TryParse(quantidadeString, out quantidade);

                        if (isValid)
                        {
                            try
                            {
                                investidorAtual.VenderMoeda(nomeMoeda, quantidade);
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
                        Console.WriteLine("Exemplo: sell-coin Bitcoin 50");
                        Console.WriteLine("Exemplo: sell-coin Bitcoin");
                    }
                    break;
                case "show-prices":
                    if (comando.Length == 1)
                    {
                        (var nomesMoedas, var precosMoedas) = investidorAtual.ObterPrecos();

                        foreach (var a in nomesMoedas.Zip(precosMoedas, (n, p) => new { n, p }))
                        {
                            Console.WriteLine(a.n + " " + a.p);
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando show-prices não requer argumentos.");
                    }
                    break;
                case "logout":
                    investidorAtual = null;
                    Console.WriteLine("Você foi desconectado.");
                    break;
                default:
                    Console.WriteLine("Comando não é reconhecido como um comando válido");
                    break;
            }
        }

        private static void ChamarComandosAdmin(string[] comando)
        {
            switch (comando[0])
            {
                case "help":

                    if (comando.Length == 1)
                    {
                        string[] addCoin = new string[] { "add-coin -name {nome da moeda} -symbolo {simbolo}", "Introduz uma nova moeda no sistema. Exemplo: add-coin -name Bitcoin -symbol BTC" };
                        string[] removeCoin = new string[] { "remove-coin -name {nome da moeda}", "Remove uma moeda da corretora. Exemplo: remove-coin -name Bitcoin" };
                        string[] showTransactions = new string[] { "show-transactions", "Mostra todas as transações dos utilizadores, a data em que ocorreram, e o montante ganho em comissões." };
                        string[] listCoins = new string[] { "list-coins", "Lista todas as moedas registradas na corretora e a data em que foram adicionadas." };
                        string[] addInvestor = new string[] { "add-investor -name {nome do investidor}", "Adiciona um novo investidor." };
                        string[] login = new string[] { "login -u {username}", "Permite que um investidor faça login no sistema." };

                        var ajudaAdministrador = new Dictionary<string, string>
                        {
                            {addCoin[0],addCoin[1] },
                            {removeCoin[0],removeCoin[1] },
                            {showTransactions[0], showTransactions[1] },
                            {listCoins[0], listCoins[1] },
                            {addInvestor[0],addInvestor[1] },
                            {login[0],login[1] }
                        };

                        foreach (KeyValuePair<string, string> kvp in ajudaAdministrador)
                        {
                            Console.WriteLine($"{kvp.Key} => {kvp.Value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("O comando help não requer argumentos.");
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

                        var list = datasTransacoes.Zip(tiposTransacoes, (x, y) => x + " " + y).Zip(comissoesTransacoes, (x, y) => x + " " + y);

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
                        (var nomesMoedas, var datasMoedas) = administrador.ObterNomeEDatasMoedas();

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
                case "login": // aqui, a sintaxe pedida é login -u username para abranger o máximo de casos possíveis.
                    if (comando.Length == 3)
                    {
                        if (comando[1] == "-u")
                        {
                            string nomeUsuario = comando[2];
                            Investidor investidorTemporario = new Investidor();

                            // Verificar se o Investidor já existe.
                            try
                            {
                                investidorTemporario = administrador.ObterInvestidor(nomeUsuario);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            if (investidorTemporario != null)
                            {
                                // Verificar se o investidor encontrado já tem senha associada.
                                bool passwordExiste = false;

                                try
                                {
                                    passwordExiste = administrador.VerificarSeExistePassword(nomeUsuario);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                                // Se o investidor ainda não tiver senha associada, pedir que crie uma.
                                if (!passwordExiste)
                                {
                                    string password = "";
                                    while (password == "")
                                    {
                                        Console.WriteLine("Por favor, crie uma password:");
                                        password = Console.ReadLine();
                                    }

                                    investidorTemporario.CriarPassword(password);
                                    investidorAtual = investidorTemporario;

                                    Console.WriteLine("Sua password foi criada e a sua sessão foi iniciada.");
                                }
                                else // O investidor tem senha associada. Vamos pedi-la.
                                {
                                    string password = "";
                                    while (password == "")
                                    {
                                        Console.WriteLine("Por favor, introduza a sua password:");
                                        password = Console.ReadLine();
                                    }

                                    // Verificar se a senha introduzida está correta.

                                    bool passwordEstaCorreta = investidorTemporario.VerificarPassword(password);

                                    if (passwordEstaCorreta)
                                    {
                                        investidorAtual = investidorTemporario;
                                        Console.WriteLine("A sua sessão foi iniciada.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("A password introduzida está incorreta.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("O segundo elemento deve ser -u.");
                            Console.WriteLine("Exemplo: login -u Thaissa");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tem certeza de que introduziu todos os elementos necessários na ordem correta?");
                        Console.WriteLine("Exemplo: login -u Thaissa");
                    }
                    break;
                default:
                    Console.WriteLine("Comando não é reconhecido como um comando válido");
                    break;
            }
        }
    }
}
