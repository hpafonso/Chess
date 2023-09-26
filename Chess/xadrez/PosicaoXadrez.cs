using System.ComponentModel.DataAnnotations.Schema;
using tabuleiro;

namespace xadrez
{
    // Esta classe server para converter a posição em formato "Xadrez" para
    // formato da matriz usada para a posição das peças
    
    public class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao ConverterPosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }    

        public override string ToString()
        {
            return "" + coluna.ToString().ToUpper() + linha;
        }
    }
}