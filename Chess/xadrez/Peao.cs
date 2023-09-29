using tabuleiro;

namespace xadrez
{
    public class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor)
            : base(tab, cor) { }

        private bool ExisteEnimigo(Posicao pos)
        {
            Peca p = tab.RetornaPeca(pos);
            return p != null && p.cor != this.cor;
        }

        private bool Livre(Posicao pos)
        {
            return tab.RetornaPeca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // pos será a posição de destino desta peça
            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                pos.DefinirValores(posicao.linha - 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos))
                    mat[pos.linha, pos.coluna] = true;

                pos.DefinirValores(posicao.linha - 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && qteMovimentos == 0 && Livre(pos))
                    mat[pos.linha, pos.coluna] = true;

                pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteEnimigo(pos))
                    mat[pos.linha, pos.coluna] = true;

                pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteEnimigo(pos))
                    mat[pos.linha, pos.coluna] = true;
            }
            else
            {
                pos.DefinirValores(posicao.linha + 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos))
                    mat[pos.linha, pos.coluna] = true;

                pos.DefinirValores(posicao.linha + 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && qteMovimentos == 0 && Livre(pos))
                    mat[pos.linha, pos.coluna] = true;

                pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteEnimigo(pos))
                    mat[pos.linha, pos.coluna] = true;

                pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteEnimigo(pos))
                    mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }

        public override string ToString()
        {
            return "p";
        }
    }
}
