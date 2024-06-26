using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico1
{
    internal class Tabuleiro
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

        public static bool VerificarCasaSegura (int posicao)
        {
            int POS = posicao % 13;

            if (POS == 0 || POS == 8 || posicao >= 51) 
                return true;
            else
                return false;
            
        }

        public static Peao VerificarCaptura (Peao peao)
        {
            int POS = peao.Posicao;
            if (peao.EstaSeguro == true)
                return null;

            for (int i = 0; i < Jogo.QtdJogadores; i++)
            {
                Jogador jogador = Jogo.Jogadores[i];
                if (peao.Cor == jogador.Cor)
                    continue;

                for (int j = 0; j < jogador.MeusPeoes.Length; j++)
                {
                    if (jogador.MeusPeoes[j].Posicao % 13 == (POS % 13) &&
                        jogador.MeusPeoes[j].EstaSeguro == false &&
                        jogador.MeusPeoes[j].FileiraAtual == peao.FileiraAtual)
                    {
                        return jogador.MeusPeoes[j];
                    }
                }
            }
            return null;
        }
    }
}
