using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TrabalhoPratico1
{         
    class Jogo
    {
        private static Jogador[] jogadores;
        private static int qtdJogadores = 0;
        private static int qtdDadosAtuais = 0;
        private static int[] dadosAtuais = new int[50];

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

        public static int QtdDadosAtuais
        {
            get { return qtdDadosAtuais; }
            set { qtdDadosAtuais = value; }
        }

        public static int[] DadosAtuais
        {
            get { return dadosAtuais; }
            set { dadosAtuais = value; }
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
                    Console.Write("Digite quantos jogadores irão jogar (Digite 2, 3 ou 4): ");
                    qtdJogadores = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Erro - Digite um número de 2 a 4.");
                }
            } while (qtdJogadores < 2 || qtdJogadores > 4);
            Relatorio.Escrever($"Haverá {qtdJogadores} jogadores");

            Jogadores = DefinirJogadores(qtdJogadores);

            Console.Clear();

            int turno = 0;

            while (vitoria == false)
            {
                qtdDadosAtuais = 0;
                Console.Clear();
                Jogador jogadorTurno = Jogadores[turno];
                Console.WriteLine($"É o turno do jogador {jogadorTurno.Cor}!");
                Relatorio.EscreverTurno(jogadorTurno.Cor);
                Relatorio.AtualizarRelatorio();

                int qtdDados;
                int[] dados = jogadorTurno.RolarDado(out qtdDados);

                if (dados == null)
                {
                    Console.WriteLine($"Jogador {jogadorTurno.Cor} rolou 6 três vezes e passou a vez!");
                    Relatorio.Escrever("O jogador rolou 6 três vezes e perdeu a vez");
                    Relatorio.AdicionarMomentoImportante($"---> RARO! Jogador {jogadorTurno.Cor} rolou 6 três vezes e perdeu a vez!");
                    Console.ReadLine();
                }
                else
                {
                    AdicionarDados(dados, qtdDados, jogadorTurno);
                    FazerJogada(jogadorTurno);

                    Jogador JogadorVitorioso = VerificarVitoria();
                    if (JogadorVitorioso != null)
                    {
                        vitoria = true;
                        string MensagemVitoria = $"\n======== O JOGADOR {JogadorVitorioso.Cor.ToUpper()} VENCEU! ========";
                        Console.WriteLine(MensagemVitoria);
                        Relatorio.Escrever(MensagemVitoria);
                        Relatorio.AdicionarMomentoImportante($"O Jogador {JogadorVitorioso.Cor.ToUpper()} venceu a partida!");
                        Relatorio.NomeDoVencedor = JogadorVitorioso.Cor.ToUpper();
                    }
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
            Relatorio.AtualizarRelatorio();
            Console.ReadLine();
        }
        static Jogador VerificarVitoria()
        {
            for (int i = 0; i < qtdJogadores; i++)
            {
                bool terminouJogador = true;
                for (int j = 0; j < jogadores[i].MeusPeoes.Length; j++)
                {
                    if (jogadores[i].MeusPeoes[j].Terminou == false)
                    {
                        terminouJogador = false;
                        break;
                    }
                }

                if (terminouJogador == true)
                {
                    return jogadores[i];
                }
            }
            return null;
        }
        static void FazerJogada(Jogador jogador)
        {
            for (int i = 0; qtdDadosAtuais > 0; i++)
            {
                int decisao = 0;

                if (qtdDadosAtuais == 1)
                {
                    Console.WriteLine($"\nPor ser o único dado disponível, o dado {dadosAtuais[0]} está sendo acionado imediatamente!");
                    jogador.AcionarDado(dadosAtuais[0]);
                    decisao = 1;
                }
                else
                {
                    Console.WriteLine($"\nJogador {jogador.Cor}, resta usar {qtdDadosAtuais} dados:");
                    for (int j = 0; j < qtdDadosAtuais; j++)
                    {
                        Console.WriteLine($"\t{j + 1} - Dado {dadosAtuais[j]}");
                    }

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
                    } while (decisao <= 0 || decisao > qtdDadosAtuais);

                    jogador.AcionarDado(dadosAtuais[decisao - 1]);
                }

                for (int k = decisao; k < qtdDadosAtuais; k++)
                {
                    dadosAtuais[k - 1] = dadosAtuais[k];
                }
                qtdDadosAtuais--;
            }
            Console.ReadLine();
        }
        public static void AdicionarDados(int[] dados, int qtdDadosRolados, Jogador jogadorTurno)
        {
            string saida = "";
            saida += $"\nJogador {jogadorTurno.Cor} tirou os seguintes dados: ";

            for (int i = 0; i < qtdDadosRolados; i++)
            {
                saida += $"{dados[i]} ";
                DadosAtuais[qtdDadosAtuais] = dados[i];
                qtdDadosAtuais++;
            }

            Relatorio.Escrever(saida);
            Console.WriteLine($"{saida}\n");
        }
        static Jogador[] DefinirJogadores(int qtdJogadores)
        {
            Jogador[] Jogadores = new Jogador[4];

            string[] coresAceitas;

            if (qtdJogadores != 2)
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
                        Console.WriteLine($"\n\tDigite a cor do {i + 1}° {((qtdJogadores == 2) ? "e 2° " : "")}jogador:\n");

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

                if (qtdJogadores != 2)
                {
                    Jogadores[i] = new Jogador(coresAceitas[decisao - 1].ToLower());
                    Relatorio.Escrever($"O {i+1}° escolheu a cor {coresAceitas[decisao - 1]}");
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
