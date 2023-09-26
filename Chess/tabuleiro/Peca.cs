namespace tabuleiro
{
    public class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public Tabuleiro tab { get; protected set; }
        public int qteMovimentos { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor) 
        { 
            this.posicao = null;
            this.tab = tabuleiro;
            this.cor = cor;
            qteMovimentos = 0;
        }
    }
}