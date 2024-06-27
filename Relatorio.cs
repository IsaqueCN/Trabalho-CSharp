using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TrabalhoPratico1
{
    internal class Relatorio
    {
        private static int contadorTurno = 1;
        private static string diretorio = "Relatorio_JogoLudo.txt";

        private static StreamWriter writer
        {
            get { return new StreamWriter(diretorio, true, Encoding.UTF8); }
        }
        public static string Diretorio
        {
            get { return diretorio; }
        }

        public static void Comecar()
        {
            StreamWriter Writer = new StreamWriter(diretorio, false, Encoding.UTF8);
            Writer.WriteLine("O jogo de LUDO começou");
            Writer.Close();
        }
        public static void Escrever(string texto)
        {
            StreamWriter Writer = writer;
            Writer.WriteLine(texto);
            Writer.Close();
        }

        public static void EscreverTurno(string nomeJogador)
        {
            StreamWriter Writer = writer;
            Writer.WriteLine($"\n======== TURNO {contadorTurno} ========\n\nO jogador {nomeJogador} está jogando.");
            contadorTurno++;
            Writer.Close();
        }
    }
}
