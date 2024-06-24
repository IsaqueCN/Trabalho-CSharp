using System;

namespace TrabalhoPratico1
{
    class Jogador
    {
        Random Random = new Random();

        private string cor;
        private string minhaFileira;
        private Peao[] meusPeoes = new Peao[4];

        public string Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        public string MinhaFileira
        {
            get { return minhaFileira.ToLower(); }
            set { minhaFileira = value.ToLower(); }
        }

        public Peao[] MeusPeoes
        {
            get { return meusPeoes; }
            set { meusPeoes = value; }
        }

        public Jogador(string fileira)
        {
            MinhaFileira = fileira;
            cor = fileira;

            MeusPeoes[0] = new Peao(this, MinhaFileira, "Peao 1");
            MeusPeoes[1] = new Peao(this, MinhaFileira, "Peao 2");
            MeusPeoes[2] = new Peao(this, MinhaFileira, "Peao 3");
            MeusPeoes[3] = new Peao(this, MinhaFileira, "Peao 4");
        }

        public void AcionarDado(int numAleatorio)
        {
            Console.WriteLine($"Dado com número {numAleatorio} está sendo acionado: ");

            if (numAleatorio == 6)
            {
                bool HaPeoesNoInicio = false;
                for (int i = 0; i < MeusPeoes.Length; i++)
                {
                    if (MeusPeoes[i].Comecou == false)
                    {
                        HaPeoesNoInicio = true;
                        break;
                    }
                }

                if (HaPeoesNoInicio) // Se o jogador pode remover um peão o inicio
                {
                    Console.WriteLine("Você rolou 6 e pode tirar um peão do ínicio:");
                    for (int i = 0; i < MeusPeoes.Length; i++)
                    {
                        if (MeusPeoes[i].Comecou == false)
                        {
                            Console.WriteLine($"\t{MeusPeoes[i].Nome}");
                        }
                    }

                    int retirarPeao;
                    do
                    {
                        Console.Write("Digite o número do Peão que você quer jogar: ");
                        retirarPeao = int.Parse(Console.ReadLine());
                    } while (retirarPeao < 1 || retirarPeao > 4);


                    MeusPeoes[retirarPeao].Mover(numAleatorio);
                    RolarDado();
                    return;
                }
            }
            // Senão

            Console.WriteLine($"Você rolou {numAleatorio}! Escolha qual dado movimentar:");

            for (int i = 0; i < MeusPeoes.Length; i++)
            {
                Console.WriteLine($"\t{MeusPeoes[i].Nome}");
            }

            int peaoEscolhido;
            do
            {
                Console.Write("Digite o número do Peão escolhido: ");
                peaoEscolhido = int.Parse(Console.ReadLine());
            } while (peaoEscolhido < 1 || peaoEscolhido > 4);

            MeusPeoes[peaoEscolhido].Mover(numAleatorio);
        }
        
        // Retorna null se tiver pegado 6 três vezes, senão retorna um vetor com os dados rolados, no vetor 0 significa fim.
        public int[] RolarDado()
        {
            int[] dadosRolados = new int[3];
            int contador = 0;
            int dado;

            while ((dado = Random.Next(1,7)) == 6 && contador < 3)
            {
                dadosRolados[contador] = dado;
                contador++;
            }

            if (contador == 3)
                return null;

            dadosRolados[contador] = dado;
            return dadosRolados;
        }
    }
    class Peao
    {
        private string nome;
        private Jogador meuJogador;
        private string minhaFileira;
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

        public string MinhaFileira
        {
            get { return minhaFileira.ToLower(); }
        }

        public string Cor
        {
            get { return cor; }
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


        public Peao(Jogador Jogador, string Fileira, string Nome)
        {
            meuJogador = Jogador;
            cor = Fileira;
            minhaFileira = Fileira;
            FileiraAtual = Fileira;
            nome = Nome;
        }

        public void Mover(int casas)
        {
            if (Comecou == false)
            {
                posicao = 0;
                fileiraAtual = minhaFileira;
                Console.WriteLine($"Peão {Nome} retirado do ínicio da fileira {MinhaFileira}");
            }
            else
            {
                int POS = posicao % 13;
                
                if (POS + casas >= 13)
                {
                    fileiraAtual = Tabuleiro.EncontrarProximaFileira(fileiraAtual);
                    Console.WriteLine($"Peão {Nome} moveu para a fileira {fileiraAtual}!");
                }
                posicao += casas;
                Console.WriteLine($"Peão {Nome} está agora na posição {posicao}");
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
            Tabuleiro tabuleiro = new Tabuleiro();
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


            // Definir a cor de cada jogador:
            string[] coresAceitas;

            if (qtdJogadores == 4)
                coresAceitas = new string[] { "Amarelo", "Azul", "Vermelho", "Verde" };
            else
                coresAceitas = new string[] { "Amarelo e Vermelho", "Vermelho e Amarelo", "Verde e Azul", "Azul e Verde" };

            for (int i = 0; i < qtdJogadores; i++)
            {
                int cor = 0;
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Bem vindo ao jogo Ludo!\n");
                        Console.WriteLine($"Você irá jogar com {qtdJogadores} jogadores!");
                        Console.WriteLine($"\n\tDigite a cor do {i + 1}° {((qtdJogadores != 4) ? "e 2° ": "")}jogador:\n");

                        for (int j = 0; j < coresAceitas.Length; j++)
                        {
                            if (coresAceitas[j] != null)
                            {
                                Console.WriteLine($"{j + 1} - {coresAceitas[j]}");
                            }
                        }

                        Console.Write("\nDigite o número da cor escolhida: ");
                        cor = int.Parse(Console.ReadLine());
                    }
                    catch 
                    {
                        Console.WriteLine("Erro - Digite um dos números na lista de cores.");
                        Console.ReadLine();
                    }
                } while (cor <= 0 || cor > 4-i);

                if (qtdJogadores == 4)
                {
                    Jogadores[i] = new Jogador(coresAceitas[cor - 1].ToLower());
                } else
                {
                    string[] coresEscolhidas = coresAceitas[cor - 1].Split(' ');
                    string cor1 = coresEscolhidas[0].ToLower();
                    string cor2 = coresEscolhidas[2].ToLower();

                    Jogadores[0] = new Jogador(cor1);
                    Jogadores[1] = new Jogador(cor2);
                    break;
                }

                coresAceitas[cor-1] = null;

                for (int k = cor; k < coresAceitas.Length - i; k++)
                {
                    coresAceitas[k - 1] = coresAceitas[k];
                    coresAceitas[k] = null;
                }
            }

            Console.Clear();

            int turno = 0;
            while (vitoria == false)
            {
                int[] dados = {-1, -1, -1};

                Console.WriteLine($"É o turno do jogador {Jogadores[turno].Cor}!");
                Console.ReadLine();

                /*
                int contador = 0;
                while (Jogadores[turno].RolarDado == 6)
                {
                    dados[contador] = 
                }
                */
                if (turno + 1 >= qtdJogadores)
                {
                    turno = 0;
                } else
                {
                    turno++;
                }
            }
            Console.ReadLine();
        }
    }
}
