using System;
using tabuleiro;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao P = new Posicao(3, 4);

            System.Console.WriteLine("Posicao: " + P);

            Console.Read();
        }
    }
} 