// Código adaptado daqui: https://stackoverflow.com/questions/11140884/command-line-interface-inside-your-c-sharp-application

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

                        administrador.AdicionarMoeda(nome, simbolo);
                    }
                    else
                    {
                        Console.WriteLine("Tem certeza de que introduziu o nome da moeda corretamente?");
                        Console.WriteLine("O nome da moeda não pode conter espaços.");
                    }
                } 
            }
            break;
    }

}
