using System;
using tabuleiro;
using xadrez;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 1));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 2));

            Tela.ImprimirTabuleiro(tab);

            Console.Read();
        }
    }
}
