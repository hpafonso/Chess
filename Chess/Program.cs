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
                PartidaDeXadrex partida = new PartidaDeXadrex();

                while (!partida.partidaTerminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);
                    System.Console.WriteLine();

                    System.Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicao();
                    // Thread.Sleep(1000);

                    bool[,] posPossiveis = partida.tab.RetornaPeca(origem).MovimentosPossiveis();
                    Console.Clear();

                    Tela.ImprimirTabuleiro(partida.tab, posPossiveis);
                    System.Console.WriteLine();

                    System.Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicao();
                    partida.ExecutaMovimento(origem, destino);
                }
            }
            catch (TabuleiroException e)
            {
                throw new TabuleiroException(e.Message);
            }

            Console.Read();
        }
    }
}
