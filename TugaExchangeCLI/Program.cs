// Código adaptado daqui: https://stackoverflow.com/questions/11140884/command-line-interface-inside-your-c-sharp-application
using System;

// Precisamos chamar TugaExchangeCLI porque é lá que se encontra
// a classe "ComandosTransversais".
using TugaExchangeCLI;

Administrador administrador = new Administrador();

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
    switch (comando[0])
    {
        case "help":
            Console.WriteLine("Texto temporário");
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
            break;
        case "show-transactions":
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

                foreach (var a in nomesMoedas.Zip(datasMoedas, (n, d) => new {n, d }))
                {
                    Console.WriteLine(a.n + " "+ a.d);
                }
            }
            break;
    }

}
