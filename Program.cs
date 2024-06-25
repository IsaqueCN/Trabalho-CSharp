using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TrabalhoPratico1
{
    class Jogador
    {
        static Random Random = new Random();

        private string cor;
        private Peao[] meusPeoes = new Peao[4];

        public string Cor
        {
            get { return cor.ToLower(); }
            set { cor = value.ToLower(); }
        }

        public Peao[] MeusPeoes
        {
            get { return meusPeoes; }
            set { meusPeoes = value; }
        }

        public Jogador(string fileira)
        {
            cor = fileira;

            MeusPeoes[0] = new Peao(this, cor, "Peao 1");
            MeusPeoes[1] = new Peao(this, cor, "Peao 2");
            MeusPeoes[2] = new Peao(this, cor, "Peao 3");
            MeusPeoes[3] = new Peao(this, cor, "Peao 4");
        }

        // Retorna null se não houver peões presos, senão retorna vetor e qtd de peões presos
        public Peao[] PeoesPresos(out int contador)
        {
            Peao[] peoesPresos = new Peao[4];

            contador = 0;
            for (int i = 0; i < meusPeoes.Length; i++)
            {
                if (meusPeoes[i].Comecou == false)
                {
                    peoesPresos[contador] = meusPeoes[i];
                    contador++;
                }
            }

            if (contador == 0)
                return null;
            else
                return peoesPresos;
        }

        public Peao[] PeoesLivres(out int contador)
        {
            Peao[] peoesLivres = new Peao[4];

            contador = 0;
            for (int i = 0; i < meusPeoes.Length; i++)
            {
                if (meusPeoes[i].Comecou == true)
                {
                    peoesLivres[contador] = meusPeoes[i];
                    contador++;
                }
            }

            if (contador == 0)
                return null;
            else
                return peoesLivres;
        }
        public void AcionarDado(int valor)
        {
            Console.WriteLine($"\nDado {valor}:");

            int decisao = 0;

            int qtdPeoesPresos;
            Peao[] peoesPresos = PeoesPresos(out qtdPeoesPresos);
            int qtdPeoesLivres;
            Peao[] peoesLivres = PeoesLivres(out qtdPeoesLivres);
            int adicionar1 = (peoesLivres == null) ? 0 : 1;

            if (valor == 6 && peoesPresos != null)
            {

                Console.WriteLine("Ações possíveis com este dado:");

                if (peoesLivres != null)
                    Console.WriteLine("\t1 - Movimentar algum peão");

                for (int i = 0; i < qtdPeoesPresos; i++)
                {
                    Console.WriteLine($"\t{i + 1 + adicionar1} - Tirar {peoesPresos[i].Nome} da prisão");
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
                        Console.WriteLine("Error - Escolha um número de escolha.");
                    }
                } while (decisao <= 0 || decisao > qtdPeoesPresos + adicionar1);

                if (!(decisao == 1 && peoesLivres != null))
                {
                    peoesPresos[decisao - 1 - adicionar1].Mover(6);
                    return;
                }
            }

            decisao = 0;
            if (peoesLivres == null)
            {
                Console.WriteLine("Você ainda não pode fazer nada com este dado!");
                return;
            }

            if (qtdPeoesLivres == 1)
            {
                Console.WriteLine($"O dado será executado no {peoesLivres[0].Nome} por ser a única escolha!");
                peoesLivres[0].Mover(valor);
            }
            else
            {
                Console.WriteLine("Escolha qual peão você deseja movimentar: ");

                for (int i = 0; i < qtdPeoesLivres; i++)
                {
                    Console.WriteLine($"\t{i + 1} - {peoesLivres[i].Nome}");
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
                        Console.WriteLine("Erro - Escolha um número de escolha.");
                    }
                } while (decisao <= 0 || decisao > qtdPeoesLivres);

                peoesLivres[decisao - 1].Mover(valor);
            }
        }

        // Retorna null se tiver pegado 6 três vezes, senão retorna um vetor com os dados rolados, no vetor 0 significa fim.
        public int[] RolarDado(out int contador)
        {
            int[] dadosRolados = new int[3];
            contador = 0;
            int dado;

            while ((dado = Random.Next(1, 7)) == 6 && contador < 3)
            {
                dadosRolados[contador] = dado;
                contador++;
            }

            if (contador == 3)
                return null;

            dadosRolados[contador] = dado;
            contador++;
            return dadosRolados;
        }
    }
    class Peao
    {
        private string nome;
        private Jogador meuJogador;
        private string fileiraAtual;
        private string cor;
        private bool comecou = false;
        private int posicao = -1;

        public string Nome
        {
            get { return nome; }
        }

        public Jogador MeuJogador
        {
            get { return meuJogador; }
        }

        public string Cor
        {
            get { return cor.ToLower(); }
        }

        public string FileiraAtual
        {
            get { return fileiraAtual.ToLower(); }
            set { fileiraAtual = value.ToLower(); }
        }
        public bool Comecou
        {
            get { return comecou; }
            set { comecou = value; }
        }
        public int Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }


        public Peao(Jogador Jogador, string Cor, string Nome)
        {
            Cor = Cor.ToLower();

            meuJogador = Jogador;
            cor = Cor;
            FileiraAtual = Cor;
            nome = Nome;
        }

        public void Mover(int casas)
        {
            if (Comecou == false)
            {
                if (casas == 6)
                {
                    posicao = 0;
                    fileiraAtual = cor;
                    comecou = true;
                    Console.WriteLine($"\n---> {Nome} {Cor} foi retirado da prisão!");
                }
            }
            else
            {
                int POS = posicao % 13;

                if (POS + casas >= 13)
                {
                    fileiraAtual = Tabuleiro.EncontrarProximaFileira(fileiraAtual);
                    Console.WriteLine($"\n---> {Nome} {Cor} está agora na fileira com cor: {fileiraAtual.ToUpper()}!");
                }
                posicao += casas;
                Console.WriteLine($"\n---> {Nome} {Cor} moveu para a posição {posicao}!");
            }
        }
    }

    class Tabuleiro
    {
        public static string EncontrarProximaFileira(string fileira)
        {
            fileira = fileira.ToLower();
            switch (fileira)
            {
                case "amarelo": return "azul";
                case "azul": return "vermelho";
                case "vermelho": return "verde";
                case "verde": return "amarelo";
                default: return null;
            }
        }
    }
    class Jogo
    {
        static void Main(string[] args)
        {
            bool vitoria = false;
            Jogador[] Jogadores = new Jogador[4];

            int qtdJogadores = 0;
            Console.WriteLine("Bem vindo ao jogo Ludo!\n");

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

            Jogadores = DefinirJogadores(qtdJogadores);

            Console.Clear();

            int turno = 0;

            while (vitoria == false)
            {
                Console.Clear();
                Jogador jogadorTurno = Jogadores[turno];
                Console.WriteLine($"É o turno do jogador {jogadorTurno.Cor}!");

                int qtdDados;
                int[] dados = jogadorTurno.RolarDado(out qtdDados);

                if (dados == null)
                {
                    Console.WriteLine($"Jogador {jogadorTurno.Cor} rolou 6 três vezes e passou a vez!");
                }
                else
                {
                    Console.Write($"Jogador {jogadorTurno.Cor} tirou os seguintes dados: ");

                    for (int i = 0; i < qtdDados; i++)
                    {
                        Console.Write($"{dados[i]} ");
                    }

                    Console.WriteLine();
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
                }
                else
                {
                    string[] coresEscolhidas = coresAceitas[decisao - 1].Split(' ');
                    string cor1 = coresEscolhidas[0].ToLower();
                    string cor2 = coresEscolhidas[2].ToLower();

                    Jogadores[0] = new Jogador(cor1);
                    Jogadores[1] = new Jogador(cor2);
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
