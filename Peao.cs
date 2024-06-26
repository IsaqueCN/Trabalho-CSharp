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
        private bool estaSeguro = true;
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
        public bool EstaSeguro
        {
            get { return estaSeguro; }
            set { estaSeguro = value; }
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

        public void Prender()
        {
            posicao = 0;
            estaLivre = false;
            fileiraAtual = cor;
            string saida = $"---> {Nome} {Cor} voltou para a prisão";
            Console.WriteLine($"{saida}");
            Relatorio.Escrever(saida);
        }
        public void Mover(int casas)
        {
            string saida = "";
            if (EstaLivre == false)
            {
                if (casas == 6)
                {
                    posicao = 0;
                    fileiraAtual = cor;
                    EstaLivre = true;
                    saida = $"---> {Nome} {Cor} foi retirado da prisão!";
                }
            }
            else
            {
                
                int POS = posicao % 13;

                saida = $"---> {Nome} {Cor} moveu para a posição {posicao + casas}!";
                if (POS + casas >= 13)
                {
                    fileiraAtual = Tabuleiro.EncontrarProximaFileira(fileiraAtual);
                    saida += $"\n---> {Nome} {Cor} está agora na fileira com cor: {fileiraAtual.ToUpper()}!";
                }
                posicao += casas;
            }
            Console.WriteLine($"\n{saida}");
            Relatorio.Escrever(saida);

            DefinirSeguranca(posicao);
            TentarCaptura(posicao);
        }

        public void DefinirSeguranca(int posicao)
        {
            if (Tabuleiro.VerificarCasaSegura(posicao))
            {
                if (estaSeguro == false)
                {
                    EstaSeguro = true;
                    string saida = $"---> {Nome} {Cor} agora está seguro!";
                    Console.WriteLine(saida);
                    Relatorio.Escrever(saida);
                }
            }
            else if (estaSeguro == true)
            {
                EstaSeguro = false;
                string saida = $"---> {Nome} {Cor} não está mais seguro!";
                Console.WriteLine(saida);
                Relatorio.Escrever(saida);
            }
        }

        public void TentarCaptura(int posicao)
        {
            string saida = "";
            if (estaSeguro == false)
            {
                Peao PeaoCapturado = Tabuleiro.VerificarCaptura(this);
                if (PeaoCapturado != null)
                {
                    saida += $"---> {Nome} {Cor} capturou o {PeaoCapturado.Nome} {PeaoCapturado.Cor}!";
                    Console.WriteLine(saida);
                    Relatorio.Escrever(saida);

                    PeaoCapturado.Prender();
                }
            }
        }
    }
}
