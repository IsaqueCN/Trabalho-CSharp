using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico1
{
    internal class Jogador
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
        public Peao[] PeoesPresos(out int qtdPeoesPresos)
        {
            Peao[] peoesPresos = new Peao[4];

            qtdPeoesPresos = 0;
            for (int i = 0; i < meusPeoes.Length; i++)
            {
                if (meusPeoes[i].EstaLivre == false)
                {
                    peoesPresos[qtdPeoesPresos] = meusPeoes[i];
                    qtdPeoesPresos++;
                }
            }

            if (qtdPeoesPresos == 0)
                return null;
            else
                return peoesPresos;
        }

        public Peao[] PeoesLivres(out int qtdPeoesLivres)
        {
            Peao[] peoesLivres = new Peao[4];

            qtdPeoesLivres = 0;
            for (int i = 0; i < meusPeoes.Length; i++)
            {
                if (meusPeoes[i].EstaLivre == true)
                {
                    peoesLivres[qtdPeoesLivres] = meusPeoes[i];
                    qtdPeoesLivres++;
                }
            }

            if (qtdPeoesLivres == 0)
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
}
