using System;
using tabuleiro;
using xadrez;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('a', 2);
            System.Console.WriteLine(pos.ConverterPosicao());
            Console.Read();
        }
    }
}
