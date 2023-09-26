using tabuleiro;

namespace xadrez
{
    public class Peao : Peca 
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "p";
        }
    }
}