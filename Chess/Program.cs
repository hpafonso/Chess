using System;
using tabuleiro;
using xadrez;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine();
            try
            {
                PartidaDeXadrex partida = new PartidaDeXadrex();

                while (!partida.partidaTerminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicao();
                        partida.ValidarPosicaoOrigem(origem);

                        bool[,] posPossiveis = partida.tab.RetornaPeca(origem).MovimentosPossiveis();
                        Console.Clear();

                        Tela.ImprimirTabuleiro(partida.tab, posPossiveis);
                        Console.WriteLine();
                        Console.WriteLine($"\nTurno: {partida.turno}");
                        Console.WriteLine($"Aguardando jogada: {partida.jogadorAtual}");

                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);
                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);
            }
            catch (TabuleiroException e)
            {
                throw new TabuleiroException(e.Message);
            }

            Console.Read();
        }
    }
}
