using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace Chess
{
    public class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrex partida)
        {
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine($"\nTurno: {partida.turno}");
            if (!partida.partidaTerminada)
            {
                Console.WriteLine($"Aguardando jogada: {partida.jogadorAtual}");
                if (partida.xeque)
                    Console.WriteLine($"XEQUE!");
            }
            else 
            {
                Console.WriteLine($"XEQUEMATE!\n");
                Console.WriteLine($"Vencedor: {partida.jogadorAtual}");
                
            }
            
        }

        // Imprime tanto as peças pretas como as brancas que foram capturadas
        public static void ImprimirPecasCapturadas(PartidaDeXadrex partida)
        {
            Console.WriteLine($"Peças capturadas: ");
            Console.Write($"Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.Write($"Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
        }
        
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write($"[");
            foreach (var p in conjunto)
                Console.Write(p + ", ");
            Console.WriteLine($"]");
            
            
        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            // O método "ImprimirTabuleiro()" recebe um tabuleiro (que terá de ser instanciado no main)
            // Após isso, percorre a matriz e receber o valor de cada posição através do metodo "Peca()"
            // É feita uma verificação para ver se na posição atual já tem alguma peça
            // Se não, é impresso um "- "

            // bullet point symbol
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string bulletPoint = "\u2022";

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write($"{8 - i} {bulletPoint} ");
                for (int j = 0; j < tab.colunas; j++) 
                { 
                    ImprimirPeca(tab.RetornaPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.Write(
                $"    {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint}"
            );
            Console.Write($"\n    A B C D E F G H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posPossiveis)
        {
            // bullet point symbol
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string bulletPoint = "\u2022";

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write($"{8 - i} {bulletPoint} ");
                for (int j = 0; j < tab.colunas; j++) 
                { 
                    if (posPossiveis[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else Console.BackgroundColor = fundoOriginal;
                    ImprimirPeca(tab.RetornaPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.Write(
                $"    {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint} {bulletPoint}"
            );
            Console.Write($"\n    A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            if (s == null || s.Length > 2)
                throw new TabuleiroException("Por favor introduza uma posição válida.");
            return new PosicaoXadrez(
                Convert.ToChar(s[0]),
                Convert.ToInt32(Char.GetNumericValue(s[1]))
            );
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
                Console.Write("- ");
            else if (peca.cor == Cor.Branca)
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
