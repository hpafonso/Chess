using tabuleiro;

namespace xadrez
{
    public class Rainha : Peca 
    {
        public Rainha(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "r";
        }
    }
}