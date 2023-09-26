using System.ComponentModel.DataAnnotations.Schema;
using tabuleiro;

namespace xadrez
{
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
            return new Posicao((8 - linha), (coluna - 'a'));
        }    

        public override string ToString()
        {
            return "" + coluna.ToString().ToUpper() + linha;
        }
    }
}