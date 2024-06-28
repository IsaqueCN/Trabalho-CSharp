using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
        private bool estaFinalizando = false;
        private bool terminou = false;
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
        public bool EstaFinalizando
        {
            get { return estaFinalizando; }
            set { estaFinalizando = value; }
        }
        public bool Terminou
        {
            get { return terminou; }
            set { terminou = value; }
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
            posicao = -1;
            estaLivre = false;
            fileiraAtual = cor;
            string saida = $"---> {Nome} {Cor} voltou para a prisão";
            Console.WriteLine(saida);
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
                posicao += casas;

                saida = $"---> {Nome} {Cor} moveu para a posição {posicao}!";
                if (terminou == false)
                {                   
                    saida = VerificarFinalizou(saida);
                    if (estaFinalizando == true)
                    {
                        Console.WriteLine($"\n{saida}");
                        Relatorio.Escrever(saida);
                        return;
                    }
                }

                if (POS + casas >= 13)
                {
                    fileiraAtual = Tabuleiro.EncontrarProximaFileira(fileiraAtual);
                    saida += $"\n---> {Nome} {Cor} está agora na fileira com cor: {fileiraAtual.ToUpper()}!";
                }
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
                    saida += $"---> {Nome} {Cor} CAPTUROU o {PeaoCapturado.Nome} {PeaoCapturado.Cor}!";
                    Console.WriteLine(saida);
                    Relatorio.Escrever(saida);
                    Relatorio.AdicionarMomentoImportante($"---> {Nome} {Cor} CAPTUROU o {PeaoCapturado.Nome} {PeaoCapturado.Cor}!");

                    PeaoCapturado.Prender();
                }
            }
        }

        public string VerificarFinalizou(string saida)
        {
            int restamParaGanhar = 56 - posicao;
            if (posicao >= 51)
            {
                if (estaFinalizando == false)
                {
                    saida += $"\n---> {Nome} {Cor} está agora na RETA FINAL!!";
                    Relatorio.AdicionarMomentoImportante($"---> {Nome} {Cor} está agora na RETA FINAL");

                    saida += $"\n---> Faltam {restamParaGanhar} casas para o {Nome} {Cor} terminar";
                    estaFinalizando = true;
                    DefinirSeguranca(posicao);
                }
                else
                {
                    if (restamParaGanhar == 0)
                    {
                        saida += $"\n---> {Nome} {Cor} CHEGOU AO FINAL!!";
                        Relatorio.AdicionarMomentoImportante($"***> {Nome} {Cor} CHEGOU AO FINAL!!");
                        terminou = true;  
                    }
                    else
                    {
                        saida += $"\n---> Faltam {restamParaGanhar} casas para o {Nome} {Cor} terminar";
                    }
                }
            }
            return saida;
        }
    }
}
