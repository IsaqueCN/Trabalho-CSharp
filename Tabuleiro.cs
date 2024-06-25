using System;
using System.Collections.Generic;
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
    }
}
