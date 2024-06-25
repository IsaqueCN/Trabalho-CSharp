using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico1
{
    internal class Peao
    {
        private string nome;
        private Jogador meuJogador;
        private string fileiraAtual;
        private string cor;
        private bool estaLivre = false;
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
        public bool EstaLivre
        {
            get { return estaLivre; }
            set { estaLivre = value; }
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
            if (EstaLivre == false)
            {
                if (casas == 6)
                {
                    posicao = 0;
                    fileiraAtual = cor;
                    EstaLivre = true;
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
}
