using tabuleiro;

namespace Chess
{
    public class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            // O método "ImprimirTabuleiro()" recebe um tabuleiro (que terá de ser instanciado no main)
            // Após isso, percorre a matriz e receber o valor de cada posição através do metodo "Peca()"
            // É feita uma verificação para ver se na posição atual já tem alguma peça
            // Se não, é impresso um "- "
            
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.RetornaPeca(i, j) == null)
                        Console.Write("- ");
                    else 
                    {
                        ImprimirPeca(tab.RetornaPeca(i, j));
                    }
                }
                Console.WriteLine();
            }
            Console.Write($"  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
                Console.Write(peca + " ");
            else 
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;
            }
        }
    }
}