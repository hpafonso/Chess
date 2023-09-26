using System;
using tabuleiro;
using xadrez;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(1, 5));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(2, 5));

                Tela.ImprimirTabuleiro(tab);
            }
            catch (TabuleiroException e)
            {
                throw new TabuleiroException(e.Message);
            }

            Console.Read();
        }
    }
}
