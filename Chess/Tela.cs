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
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.Peca(i, j) == null)
                        System.Console.Write("- ");
                    else System.Console.Write(tab.Peca(i, j) + " ");
                }
                System.Console.WriteLine();
            }
        }
    }
}