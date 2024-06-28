using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace TrabalhoPratico1
{
    internal class Relatorio
    {
        private static int contadorCapturas = 0;
        private static int contadorRetaFinal = 0;
        private static int contadorFinalistas = 0;
        private static int contadorMomentosImportantes = 0;
        private static int contadorTurno = 0;

        private static string diretorio = "Relatorio_JogoLudo.txt";
        private static string nomeDoVencedor;
        private static string relatorioGeral = "----- RELATORIO GERAL -----\n\n";
        private static string momentosImportantes = "----- MOMENTOS IMPORTANTES -----\n\n";
        private static string andamentoDoJogo = "----- ANDAMENTO DO JOGO -----\n\n";
        private static bool mudancasPendentes = false;
        //É uma propriedade facilitadora que retorna um novo StreamWriter para adicionar uma nova escrita no arquivo. 
        public static int ContadorCapturas
        {
            get { return contadorCapturas; }
        }
        public static int ContadorRetaFinal
        {
            get { return contadorRetaFinal; }
        }
        public static int ContadorFinalistas
        {
            get { return contadorFinalistas; }
        }
        public static int ContadorMomentosImportantes
        {
            get { return contadorMomentosImportantes; }
        }
        public static int ContadorTurno
        {
            get { return contadorTurno; }
        }
        public static string Diretorio
        {
            get { return diretorio; }
        }
        public static string NomeDoVencedor
        {
            get { return nomeDoVencedor; }
            set { nomeDoVencedor = value; }
        }
        private static StreamWriter writerAdicionar
        {
            get { return new StreamWriter(diretorio, true, Encoding.UTF8); }
        }
        private static StreamWriter writerSubstituir
        {
            get { return new StreamWriter(diretorio, false, Encoding.UTF8); }
        }

        //Limpa o relatório e começa um novo
        public static void Comecar()
        {
            StreamWriter Writer = writerSubstituir;
            string texto = "O jogo de LUDO começou";
            Writer.WriteLine(texto);
            andamentoDoJogo += $"{texto}\n";

            Writer.Close();
        }
        //Quando chamada escreve qualquer texto passada como parametro.
        public static void Escrever(string texto)
        {
            StreamWriter Writer = writerAdicionar;
            Writer.WriteLine(texto);
            andamentoDoJogo += $"{texto}\n";

            Writer.Close();
        }

        public static void EscreverTurno(string nomeJogador)
        {
            StreamWriter Writer = writerAdicionar;
            contadorTurno++;
            string texto = $"\n======== TURNO {contadorTurno} ========\n\nO jogador {nomeJogador} está jogando.";
            Writer.WriteLine(texto);
            andamentoDoJogo += $"{texto}\n";

            Writer.Close();
        }

        public static void AtualizarRelatorio()
        {
            StreamWriter Writer = writerSubstituir;
            if (contadorMomentosImportantes > 0)
                Writer.WriteLine($"{ReceberRelatorioGeral()}\n{momentosImportantes}\n{andamentoDoJogo}");
            else
                Writer.WriteLine($"{ReceberRelatorioGeral()}\n{momentosImportantes}Não há momentos importantes\n\n{andamentoDoJogo}");

            Writer.Close();
        }
        public static void AdicionarMomentoImportante(string texto)
        {
            momentosImportantes += $"TURNO {contadorTurno}: {texto}\n";
            mudancasPendentes = true;

            contadorMomentosImportantes++;

            texto = texto.ToUpper();

            if (texto.Contains("RETA FINAL"))
                contadorRetaFinal++;
            else if (texto.Contains("CHEGOU AO FINAL"))
                contadorFinalistas++;
            else if (texto.Contains("CAPTUROU"))
                contadorCapturas++;
        }

        public static string ReceberRelatorioGeral()
        {
            string capturasTexto, retaFinalTexto, finalistaTexto, turnosTexto, importantesTexto, vitoriosoTexto;

            if (nomeDoVencedor == null)
                vitoriosoTexto = "Não há vencedor";
            else
                vitoriosoTexto = $"O Jogador {nomeDoVencedor} venceu o jogo";

            if (contadorCapturas == 1)
                capturasTexto = "1 Peão foi capturado";
            else
                capturasTexto = $"{contadorCapturas} Peões foram capturados";

            if (contadorRetaFinal == 1)
                retaFinalTexto = "1 Peão chegou na reta final";
            else
                retaFinalTexto = $"{contadorRetaFinal} Peões chegaram na reta final";

            if (contadorFinalistas == 1)
                finalistaTexto = "1 Peão finalizou o percurso";
            else
                finalistaTexto = $"{contadorFinalistas} Peões finalizaram o percurso";

            if (contadorTurno == 1)
                turnosTexto = "1 Turno aconteceu";
            else
                turnosTexto = $"{contadorTurno} Turnos aconteceram";

            if (contadorMomentosImportantes == 1)
                importantesTexto = "1 Momento importante ocorreu";
            else
                importantesTexto = $"{contadorMomentosImportantes} Momentos importantes ocorreram";

            return relatorioGeral + $"{vitoriosoTexto}\n{turnosTexto}\n{importantesTexto}\n{capturasTexto}\n{retaFinalTexto}\n{finalistaTexto}\n";
        }
    }
}
