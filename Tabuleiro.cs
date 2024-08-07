﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPratico1
{
    /// <summary>
    /// Fornece funções uteis relacionadas ao tabuleiro do jogo Ludo.
    /// </summary>
    internal class Tabuleiro
    {
        /// <summary>
        /// Encontra a próxima fileira no tabuleiro Ludo.
        /// </summary>
        /// <returns>Próxima fileira em relação a fileira recebida</returns>
        public static string EncontrarProximaFileira(string nomeDaFileira)
        {
            nomeDaFileira = nomeDaFileira.ToLower();
            switch (nomeDaFileira)
            {
                case "amarelo": return "azul";
                case "azul": return "vermelho";
                case "vermelho": return "verde";
                case "verde": return "amarelo";
                default: return null;
            }
        }

        /// <summary>
        /// Verifica se a posição recebida é uma casa segura.
        /// </summary>
        public static bool VerificarCasaSegura (int posicao)
        {
            //Obtem a posição do peão na sua fileira atual
            int POS = posicao % 13;

            //0 - começo da fileira
            //8 - estrela
            // >=51 reta final
            if (POS == 0 || POS == 8 || posicao >= 51) 
                return true;
            else
                return false;
            
        }

        /// <summary>
        /// Verifica se determinado peão capturou algum outro peão.
        /// </summary>
        /// <returns>Peão capturado ou null se não houver.</returns>
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
                        jogador.MeusPeoes[j].FileiraAtual == peao.FileiraAtual &&
                        jogador.MeusPeoes[j].EstaFinalizando == false)
                    {
                        return jogador.MeusPeoes[j];
                    }
                }
            }
            return null;
        }
    }
}
