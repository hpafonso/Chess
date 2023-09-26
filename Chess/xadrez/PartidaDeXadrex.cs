using System;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrex
    {
        public Tabuleiro tab {get; private set;}
        private int _turno;
        private Cor _jogadorAtual;
        public bool partidaTerminada { get; private set;}

        public PartidaDeXadrex()
        {
            tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
            ColocarPecas();
            partidaTerminada = false;
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarMovimento();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('a', 8).ConverterPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('b', 8).ConverterPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('c', 8).ConverterPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).ConverterPosicao());
            tab.ColocarPeca(new Rainha(tab, Cor.Preta), new PosicaoXadrez('e', 8).ConverterPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('f', 8).ConverterPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('g', 8).ConverterPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('h', 8).ConverterPosicao());

            for (int i = Convert.ToInt32('a'); i < Convert.ToInt32('a') + 8; i++)
            {
                tab.ColocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez(Convert.ToChar(i), 7).ConverterPosicao());
            }

            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('a', 1).ConverterPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('b', 1).ConverterPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('c', 1).ConverterPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).ConverterPosicao());
            tab.ColocarPeca(new Rainha(tab, Cor.Branca), new PosicaoXadrez('e', 1).ConverterPosicao());
            tab.ColocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('f', 1).ConverterPosicao());
            tab.ColocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('g', 1).ConverterPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('h', 1).ConverterPosicao());

            for (int i = Convert.ToInt32('a'); i < Convert.ToInt32('a') + 8; i++)
            {
                tab.ColocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez(Convert.ToChar(i), 2).ConverterPosicao());
            }
        }
    }
}
