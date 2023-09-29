using System.Reflection.Metadata.Ecma335;
using tabuleiro;

namespace xadrez
{
    public class Rei : Peca
    {
        PartidaDeXadrex partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrex partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.RetornaPeca(pos);
            return p == null || p.cor != this.cor;
        }

        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = tab.RetornaPeca(pos);
            return p != null && p is Torre && p.cor == this.cor && p.qteMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // pos será a posição de destino desta peça
            Posicao pos = new Posicao(0, 0);

            // Posição Acima
            pos.DefinirValores(posicao.linha - 1, posicao.coluna);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição NorthEast
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição Direita
            pos.DefinirValores(posicao.linha, posicao.coluna + 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição SouthEast
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição Abaixo
            pos.DefinirValores(posicao.linha + 1, posicao.coluna);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição SouthWest
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição Esquerda
            pos.DefinirValores(posicao.linha, posicao.coluna - 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // Posição NorthWest
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            // #JOGADAESPECIAL ROQUE
            if (qteMovimentos == 0 && !partida.xeque)
            {
                // #JODADAESPECIAL ROQUE PEQUENO
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (TesteTorreParaRoque(posT1))
                {
                    // Posições entre o rei e a torre
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    
                    if (tab.RetornaPeca(p1) == null && tab.RetornaPeca(p2) == null)
                    {
                        mat[p2.linha, p2.coluna] = true;
                    }
                }

                // #JODADAESPECIAL ROQUE GRANDE
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (TesteTorreParaRoque(posT2))
                {
                    // Posições entre o rei e a torre
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tab.RetornaPeca(p1) == null && tab.RetornaPeca(p2) == null && tab.RetornaPeca(p3) == null)
                    {
                        mat[p2.linha, p2.coluna] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
