using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TrabalhoPratico1
{         
    class Jogo
    {
        private static Jogador[] jogadores = new Jogador[4];
        private static int qtdJogadores = 0;

        public static Jogador[] Jogadores
        {
            get { return jogadores; }
            set { jogadores = value; }
        }

        public static int QtdJogadores
        {
            get { return qtdJogadores; }
            set { qtdJogadores = value; }
        }

        static void Main(string[] args)
        {
            bool vitoria = false;
            Console.WriteLine("Bem vindo ao jogo Ludo!\n");
            Relatorio.Comecar();

            do
            {
                try
                {
                    Console.Write("Digite quantos jogadores irão jogar (Digite 2 ou 4): ");
                    qtdJogadores = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Erro - Digite o número 2 ou 4.");
                }
            } while (qtdJogadores != 2 && qtdJogadores != 4);
            Relatorio.Escrever($"Haverá {qtdJogadores} jogadores");

            Jogadores = DefinirJogadores(qtdJogadores);

            Console.Clear();

            int turno = 0;

            while (vitoria == false)
            {
                Console.Clear();
                Jogador jogadorTurno = Jogadores[turno];
                Console.WriteLine($"É o turno do jogador {jogadorTurno.Cor}!");
                Relatorio.EscreverTurno(jogadorTurno.Cor);

                int qtdDados;
                int[] dados = jogadorTurno.RolarDado(out qtdDados);

                if (dados == null)
                {
                    Console.WriteLine($"Jogador {jogadorTurno.Cor} rolou 6 três vezes e passou a vez!");
                    Relatorio.Escrever("O jogador rolou 6 três vezes e perdeu a vez");
                }
                else
                {
                    string saida = "";
                    saida += $"Jogador {jogadorTurno.Cor} tirou os seguintes dados: ";

                    for (int i = 0; i < qtdDados; i++)
                    {
                        saida += $"{dados[i]} ";
                    }
                    Relatorio.Escrever(saida);
                    Console.WriteLine($"{saida}\n");
                    FazerJogada(jogadorTurno, dados, qtdDados);
                }

                // Mudar para o próximo turno
                if (turno + 1 >= qtdJogadores)
                {
                    turno = 0;
                }
                else
                {
                    turno++;
                }
            }
            Console.ReadLine();
        }
        static void FazerJogada(Jogador jogador, int[] dados, int qtdDados)
        {
            int contador = qtdDados;

            for (int i = 0; contador > 0; i++)
            {
                if (contador == 1)
                {
                    Console.WriteLine($"\nPor ser o único dado disponível, o dado {dados[0]} está sendo acionado imediatamente!");
                    jogador.AcionarDado(dados[0]);
                    contador--;
                }
                else
                {
                    Console.WriteLine($"\nJogador {jogador.Cor}, resta usar {contador} dados:");
                    for (int j = 0; j < contador; j++)
                    {
                        Console.WriteLine($"\t{j + 1} - Dado {dados[j]}");
                    }

                    int decisao = 0;

                    do
                    {
                        try
                        {
                            Console.Write("Escolha: ");
                            decisao = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Erro - Digite o número do dado que será usado.");
                        }
                    } while (decisao <= 0 || decisao > contador);

                    jogador.AcionarDado(dados[decisao - 1]);

                    for (int k = decisao; k < dados.Length - i; k++)
                    {
                        dados[k - 1] = dados[k];
                    }
                    contador--;
                }
            }
            Console.ReadLine();
        }
        static Jogador[] DefinirJogadores(int qtdJogadores)
        {
            Jogador[] Jogadores = new Jogador[4];

            string[] coresAceitas;

            if (qtdJogadores == 4)
                coresAceitas = new string[] { "Amarelo", "Azul", "Vermelho", "Verde" };
            else
                coresAceitas = new string[] { "Amarelo e Vermelho", "Vermelho e Amarelo", "Verde e Azul", "Azul e Verde" };

            for (int i = 0; i < qtdJogadores; i++)
            {
                int decisao = 0;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Bem vindo ao jogo Ludo!\n");
                        Console.WriteLine($"Você irá jogar com {qtdJogadores} jogadores!");
                        Console.WriteLine($"\n\tDigite a cor do {i + 1}° {((qtdJogadores != 4) ? "e 2° " : "")}jogador:\n");

                        for (int j = 0; j < coresAceitas.Length; j++)
                        {
                            if (coresAceitas[j] != null)
                            {
                                Console.WriteLine($"{j + 1} - {coresAceitas[j]}");
                            }
                        }

                        Console.Write("\nDigite o número da cor escolhida: ");
                        decisao = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Erro - Digite um dos números na lista de cores.");
                        Console.ReadLine();
                    }
                } while (decisao <= 0 || decisao > 4 - i);

                if (qtdJogadores == 4)
                {
                    Jogadores[i] = new Jogador(coresAceitas[decisao - 1].ToLower());
                    Relatorio.Escrever($"O {i}° escolheu a cor {coresAceitas[decisao - 1]}");
                }
                else
                {
                    string[] coresEscolhidas = coresAceitas[decisao - 1].Split(' ');
                    string cor1 = coresEscolhidas[0].ToLower();
                    string cor2 = coresEscolhidas[2].ToLower();

                    Jogadores[0] = new Jogador(cor1);
                    Jogadores[1] = new Jogador(cor2);
                    Relatorio.Escrever($"O 1° jogador escolheu a cor {coresEscolhidas[0]}\nO {2}° jogador escolheu a cor {coresEscolhidas[2]}");
                    break;
                }

                coresAceitas[decisao - 1] = null;

                for (int k = decisao; k < coresAceitas.Length - i; k++)
                {
                    coresAceitas[k - 1] = coresAceitas[k];
                    coresAceitas[k] = null;
                }
            }
            return Jogadores;
        }
    }
}
