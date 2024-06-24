using System;

namespace TrabalhoPratico1
{
    class Jogador
    {
        Random Random = new Random();

        private string cor;
        private Fileira minhaFileira;
        private Peao[] meusPeoes = new Peao[4];

        public string Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        public Fileira MinhaFileira
        {
            get { return minhaFileira; }
            set { minhaFileira = value; }
        }

        public Peao[] MeusPeoes
        {
            get { return meusPeoes; }
            set { meusPeoes = value; }
        }

        public Jogador(Fileira fileira)
        {
            MinhaFileira = fileira;
            cor = fileira.Cor;

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
        public int RolarDado()
        {
            return Random.Next(0, 7);
        }
    }
    class Peao
    {
        private string nome;
        private Jogador meuJogador;
        private Fileira minhaFileira;
        private Fileira fileiraAtual;
        private string cor;
        private bool comecou = false;
        private string posicao = "-1";

        public string Nome
        {
            get { return nome; }
        }

        public Jogador MeuJogador
        {
            get { return meuJogador; }
        }

        public Fileira MinhaFileira
        {
            get { return minhaFileira; }
        }

        public string Cor
        {
            get { return cor; }
        }

        public Fileira FileiraAtual
        {
            get { return fileiraAtual; }
            set { fileiraAtual = value; }
        }
        public bool Comecou
        {
            get { return comecou; }
            set { comecou = value; }
        }
        public string Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }


        public Peao(Jogador Jogador, Fileira Fileira, string Nome)
        {
            meuJogador = Jogador;
            minhaFileira = Fileira;
            FileiraAtual = Fileira;
            nome = Nome;
            cor = MinhaFileira.Cor;
        }

        public void Mover(int casas)
        {
            if (Comecou == false)
            {
                Posicao = MinhaFileira.MoverPeao(this, 0);
                Console.WriteLine($"Peão {Nome} retirado do ínicio da fileira {MinhaFileira.Cor}");
            }
            else
            {
                Posicao = MinhaFileira.MoverPeao(this, casas);
                Console.WriteLine($"Peão {Nome} movido.");
            }
        }
    }
    class Fileira
    {
        private string cor;
        private string[,] casas = new string[6, 3];
        private int contadorPeoes = 0;
        private Peao[] meusPeoes = new Peao[16];

        public string Cor
        {
            get { return cor; }
        }

        public string[,] Casas
        {
            get { return casas; }
            set { casas = value; }
        }

        public int ContadorPeoes
        {
            get { return contadorPeoes; }
            set { contadorPeoes = value; }
        }
        public Peao[] MeusPeoes
        {
            get { return meusPeoes; }
            set { meusPeoes = value; }
        }
        public Fileira(string Cor)
        {
            cor = Cor;
        }

        public string MoverPeao(Peao peao, int casas)
        {
            if (casas == 0) // Se o peão está saindo do ínicio
            {
                Casas[1, 3] = ContadorPeoes.ToString();
                MeusPeoes[ContadorPeoes] = peao;
                ContadorPeoes++;
                return "1 3";
            }
            else
            {
                string[] posicao = peao.Posicao.Split(' ');
                int linha = int.Parse(posicao[0]);
                int coluna = int.Parse(posicao[1]);

                if (coluna == 1)
                {
                    // Se está vindo
                }

                if (coluna == 2)
                {
                    // Se está no meio
                }

                if (coluna == 3)
                {
                    int Novalinha = linha + casas;
                    int resto = linha % 6;

                    if (resto > 0)
                    {
                        // Fazer o peao ir para a próxima fileira
                    }
                    else
                    {
                        Casas[linha, coluna] = "-1";
                        return Novalinha + " 3";
                    }
                }
            }

            return "0";
        }
    }

    class Tabuleiro
    {
        private Fileira[] fileiras;
        public Fileira[] Fileiras
        {
            get { return fileiras; }
        }
        public Fileira FileiraAmarela
        {
            get { return fileiras[0]; }
        }
        public Fileira FileiraAzul
        {
            get { return fileiras[1]; }
        }
        public Fileira FileiraVermelha
        {
            get { return fileiras[2]; }
        }
        public Fileira FileiraVerde
        {
            get { return fileiras[3]; }
        }

        public Fileira EncontrarFileiraPorCor(string cor)
        {
            cor = cor.ToLower();

            switch (cor)
            {
                case "amarelo": return FileiraAmarela;
                case "verde": return FileiraVerde;
                case "azul": return FileiraAzul;
                case "vermelho": return FileiraVermelha;
                default: return null;
            }
        }

        public Fileira EncontrarProximaFileira(Fileira fileira)
        {
            switch (fileira.Cor)
            {
                case "Amarela": return FileiraAzul;
                case "Azul": return FileiraVermelha;
                case "Vermelha": return FileiraVerde;
                case "Verde": return FileiraAmarela;
                default: return null;
            }
        }
        public Tabuleiro()
        {
            fileiras = new Fileira[4]
            {
                new Fileira("Amarela"),
                new Fileira("Azul"),
                new Fileira("Vermelha"),
                new Fileira("Verde"),
            };
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
                    Jogadores[i] = new Jogador(tabuleiro.EncontrarFileiraPorCor(coresAceitas[cor - 1]));
                } else
                {
                    string[] coresEscolhidas = coresAceitas[cor - 1].Split(' ');
                    string cor1 = coresEscolhidas[0];
                    string cor2 = coresEscolhidas[2];

                    Jogadores[0] = new Jogador(tabuleiro.EncontrarFileiraPorCor(cor1));
                    Jogadores[1] = new Jogador(tabuleiro.EncontrarFileiraPorCor(cor2));
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
