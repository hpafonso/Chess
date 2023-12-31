using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using tabuleiro;

namespace xadrez
{
    public class PartidaDeXadrex
    {
        public Tabuleiro tab {get; private set;}
        public int turno {get; private set;}
        public Cor jogadorAtual {get; private set;}
        public bool partidaTerminada { get; private set;}
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas; 
        public bool xeque {get; private set;}
        public Peca vulneravelEnPassant {get; private set;}

        public PartidaDeXadrex()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
            partidaTerminada = false;
            xeque = false;
            vulneravelEnPassant = null;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarMovimento();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            // #JODADAESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(destino.linha, destino.coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarMovimento();
                tab.ColocarPeca(T, destinoT);
            }

            // #JODADAESPECIAL ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(destino.linha, destino.coluna + 1);
                Peca T = tab.RetirarPeca(origemT);
                T.IncrementarMovimento();
                tab.ColocarPeca(T, destinoT);
            }

            // #JODADAESPECIAL EN PASSANT
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    else
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    pecaCapturada = tab.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException($"Se fizer essa jogada, ficará em xeque!");
            }
            
            // verifica se o adversario fica em xeque com a jogada do jogador atual
            xeque = EstaEmXeque(Adversaria(jogadorAtual));

            if (TesteXequeMate(Adversaria(jogadorAtual)))
                partidaTerminada = true;
            else
            {
                turno++;
                MudaJogador();
            }

            Peca p = tab.RetornaPeca(destino);

            // #JOGADAESPECIAL EN PASSANT
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
                vulneravelEnPassant = p;
            else vulneravelEnPassant = null;    
            
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarMovimento();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            // #JODADAESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(destino.linha, destino.coluna - 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.IncrementarMovimento();
                tab.ColocarPeca(T, origemT);
            }

            // #JODADAESPECIAL ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(destino.linha, destino.coluna + 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.IncrementarMovimento();
                tab.ColocarPeca(T, origemT);
            }

            // #JOGADAESPECIAL EN PASSANT
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao  = tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                        posP = new Posicao(3, destino.coluna);
                    else posP = new Posicao(4, destino.coluna);
                    tab.ColocarPeca(peao, posP);
                }
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.RetornaPeca(pos) == null)
                throw new TabuleiroException("Posição de origem não contém nenhuma peça.");
            if (jogadorAtual != tab.RetornaPeca(pos).cor)
                throw new TabuleiroException($"Por favor escolha uma peça da cor: {jogadorAtual}");
            if (!tab.RetornaPeca(pos).ExisteMovimentosPossiveis())
                throw new TabuleiroException($"Não existem movimentos possiveis para a peça que escolheu.");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.RetornaPeca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino não é uma posição válida!");
        }
        
        private void MudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else jogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var p in capturadas)
            {
                if (p.cor == cor)
                    aux.Add(p);
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var p in pecas)
            {
                if (p.cor == cor)
                    aux.Add(p);
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            else return Cor.Branca;
        }

        // retorna o rei da cor especificada
        private Peca Rei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                    return p;
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            // Rei da cor "cor"
            Peca R = Rei(cor);

            if (R == null)
            {
                throw new TabuleiroException($"Não existe rei {cor} no tabuleiro");
            }

            foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                    return true;
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;
            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;

                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ConverterPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            ColocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));

            for (int i = Convert.ToInt32('a'); i < Convert.ToInt32('a') + 8; i++)
                ColocarNovaPeca(Convert.ToChar(i), 7, new Peao(tab, Cor.Preta, this));

            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            ColocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));

            for (int i = Convert.ToInt32('a'); i < Convert.ToInt32('a') + 8; i++)
                ColocarNovaPeca(Convert.ToChar(i), 2, new Peao(tab, Cor.Branca, this));
        }
    }
}
