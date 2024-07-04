using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico1
{
    /// <summary>
    /// Esta classe controla todo o movimento e status do peão no tabuleiro
    /// </summary>
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

        /// <summary>
        /// Leva o peão para a posição -1 e altera EstaLivre para false
        /// </summary>
        public void Prender()
        {
            Posicao = -1;
            EstaLivre = false;
            FileiraAtual = cor;
            string saida = $"---> {Nome} {Cor} voltou para a prisão";
            Console.WriteLine(saida);
            Relatorio.Escrever(saida);
        }

        /// <summary>
        /// Move o peão no tabuleiro e executa todas as ações decorrentes de seu movimento
        /// Seja tirar da prisão, capturar, chegar na reta final ou ao final do tabuleiro
        /// </summary>
        public void Mover(int casas)
        {
            string saida = "";
            if (EstaLivre == false)
            {
                if (casas == 6)
                {
                    Posicao = 0;
                    FileiraAtual = cor;
                    EstaLivre = true;
                    saida = $"---> {Nome} {Cor} foi retirado da prisão!";
                }
            }
            else
            {
                int POS = Posicao % 13;
                Posicao += casas;

                saida = $"---> {Nome} {Cor} moveu para a posição {Posicao}!";
                if (Terminou == false)
                {                   
                    saida = VerificarFinalizou(saida);
                    if (EstaFinalizando == true)
                    {
                        Console.WriteLine($"\n{saida}");
                        Relatorio.Escrever(saida);

                        if (Terminou == true)
                        {
                            int qtdDados;
                            int[] dados = MeuJogador.RolarDado(out qtdDados);
                            if (dados != null)
                                Jogo.AdicionarDados(dados, qtdDados, MeuJogador);
                            else
                            {
                                Jogo.QtdDadosAtuais = 0;
                                Console.WriteLine($"Jogador {MeuJogador.Cor} rolou 6 três vezes e passou a vez!");
                                Relatorio.Escrever("O jogador rolou 6 três vezes e perdeu a vez");
                                Relatorio.AdicionarMomentoImportante($"---> RARO! Jogador {MeuJogador.Cor} rolou 6 três vezes e perdeu a vez!");
                            }
                        }
                        DefinirSeguranca(Posicao);
                        return;
                    }
                }

                if (POS + casas >= 13)
                {
                    FileiraAtual = Tabuleiro.EncontrarProximaFileira(FileiraAtual);
                    saida += $"\n---> {Nome} {Cor} está agora na fileira com cor: {FileiraAtual.ToUpper()}!";
                }
            }
            Console.WriteLine($"\n{saida}");
            Relatorio.Escrever(saida);

            DefinirSeguranca(Posicao);
            TentarCaptura(Posicao);
        }

        /// <summary>
        /// Verifica se o dado está atualmente seguro
        /// Define a varíavel EstaSeguro para que esteja de acordo
        /// </summary>
        public void DefinirSeguranca(int posicao)
        {
            if (Tabuleiro.VerificarCasaSegura(posicao) == true)
            {
                if (EstaSeguro == false)
                {
                    EstaSeguro = true;
                    string saida = $"---> {Nome} {Cor} agora está seguro!";
                    Console.WriteLine(saida);
                    Relatorio.Escrever(saida);
                }
            }
            else if (EstaSeguro == true)
            {
                EstaSeguro = false;
                string saida = $"---> {Nome} {Cor} não está mais seguro!";
                Console.WriteLine(saida);
                Relatorio.Escrever(saida);
            }
        }

        /// <summary>
        /// Verifica se há um peão que foi capturado na casa do tabuleiro atual
        /// Caso haja, o peão capturado será preso e o jogador rolará dados novamente
        /// </summary>
        public void TentarCaptura(int posicao)
        {
            string saida = "";
            if (EstaSeguro == false)
            {
                Peao PeaoCapturado = Tabuleiro.VerificarCaptura(this);
                if (PeaoCapturado != null)
                {
                    saida += $"---> {Nome} {Cor} CAPTUROU o {PeaoCapturado.Nome} {PeaoCapturado.Cor}!";
                    Console.WriteLine(saida);
                    Relatorio.Escrever(saida);
                    Relatorio.AdicionarMomentoImportante($"---> {Nome} {Cor} CAPTUROU o {PeaoCapturado.Nome} {PeaoCapturado.Cor}!");

                    PeaoCapturado.Prender();

                    int qtdDados;
                    int[] dados = MeuJogador.RolarDado(out qtdDados);
                    if (dados != null)
                        Jogo.AdicionarDados(dados, qtdDados, MeuJogador);
                    else
                    {
                        Jogo.QtdDadosAtuais = 0;
                        Console.WriteLine($"Jogador {MeuJogador.Cor} rolou 6 três vezes e passou a vez!");
                        Relatorio.Escrever("O jogador rolou 6 três vezes e perdeu a vez");
                        Relatorio.AdicionarMomentoImportante($"---> RARO! Jogador {MeuJogador.Cor} rolou 6 três vezes e perdeu a vez!");
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se o peão chegou à reta final ou ao final do tabuleiro
        /// Define as variáveis EstaFinalizando e Terminou para que estejam de acordo
        /// </summary>
        public string VerificarFinalizou(string saida)
        {
            int restamParaGanhar = 56 - Posicao;
            if (Posicao >= 51)
            {
                if (EstaFinalizando == false && restamParaGanhar != 0)
                {
                    saida += $"\n---> {Nome} {Cor} está agora na RETA FINAL!!";
                    Relatorio.AdicionarMomentoImportante($"---> {Nome} {Cor} está agora na RETA FINAL");

                    saida += $"\n---> Faltam {restamParaGanhar} casas para o {Nome} {Cor} terminar";
                    EstaFinalizando = true;
                }
                else
                {
                    if (restamParaGanhar == 0)
                    {
                        saida += $"\n---> {Nome} {Cor} CHEGOU AO FINAL!!";
                        Relatorio.AdicionarMomentoImportante($"***> {Nome} {Cor} CHEGOU AO FINAL!!");
                        terminou = true;
                        EstaFinalizando = true;
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
