using tabuleiro;

namespace xadrez
{
    public class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor)
            : base(tab, cor) { }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.RetornaPeca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // pos será a posição de destino desta peça
            Posicao pos = new Posicao(0, 0);

            // NorthWest
            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.RetornaPeca(pos) != null && tab.RetornaPeca(pos).cor != this.cor)
                    break;
                pos.DefinirValores(pos.linha - 1, pos.coluna - 1);
            }

            // NorthEast
            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.RetornaPeca(pos) != null && tab.RetornaPeca(pos).cor != this.cor)
                    break;
                pos.DefinirValores(pos.linha - 1, pos.coluna + 1);
            }

            // SouthWest
            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.RetornaPeca(pos) != null && tab.RetornaPeca(pos).cor != this.cor)
                    break;
                pos.DefinirValores(pos.linha + 1, pos.coluna - 1);
            }

            // SouthEast
            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.RetornaPeca(pos) != null && tab.RetornaPeca(pos).cor != this.cor)
                    break;
                pos.DefinirValores(pos.linha + 1, pos.coluna + 1);
            }
            return mat;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
