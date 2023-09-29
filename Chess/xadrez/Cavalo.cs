using tabuleiro;

namespace xadrez
{
    public class Cavalo : Peca 
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

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

            pos.DefinirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            pos.DefinirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;  

            pos.DefinirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true; 

            pos.DefinirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true; 

            pos.DefinirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            pos.DefinirValores(posicao.linha +2, posicao.coluna + 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            pos.DefinirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            pos.DefinirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tab.PosicaoValida(pos) && PodeMover(pos))
                mat[pos.linha, pos.coluna] = true;

            return mat;
        }
        public override string ToString()
        {
            return "C";
        }
    }
}