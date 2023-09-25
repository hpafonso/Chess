using System;
using tabuleiro;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao P = new Posicao(3, 4);

            Tabuleiro tab = new Tabuleiro(8, 8);

            Tela.ImprimirTabuleiro(tab);

            Console.Read();
        }
    }
} 