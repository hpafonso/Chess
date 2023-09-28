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
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarMovimento();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);
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

            turno++;
            MudaJogador();
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
            if (!tab.RetornaPeca(origem).PodeMoverPara(destino))
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ConverterPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            
            // tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('a', 8).ConverterPosicao());
            // tab.ColocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('b', 8).ConverterPosicao());
            // tab.ColocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('c', 8).ConverterPosicao());
            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            // tab.ColocarPeca(new Rainha(tab, Cor.Preta), new PosicaoXadrez('e', 8).ConverterPosicao());
            // tab.ColocarPeca(new Bispo(tab, Cor.Preta), new PosicaoXadrez('f', 8).ConverterPosicao());
            // tab.ColocarPeca(new Cavalo(tab, Cor.Preta), new PosicaoXadrez('g', 8).ConverterPosicao());
            // tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('h', 8).ConverterPosicao());

            // for (int i = Convert.ToInt32('a'); i < Convert.ToInt32('a') + 8; i++)
            // {
            //     tab.ColocarPeca(new Peao(tab, Cor.Preta), new PosicaoXadrez(Convert.ToChar(i), 7).ConverterPosicao());
            // }

            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            // tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('a', 1).ConverterPosicao());
            // tab.ColocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('b', 1).ConverterPosicao());
            // tab.ColocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('c', 1).ConverterPosicao());
            // tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).ConverterPosicao());
            // tab.ColocarPeca(new Rainha(tab, Cor.Branca), new PosicaoXadrez('e', 1).ConverterPosicao());
            // tab.ColocarPeca(new Bispo(tab, Cor.Branca), new PosicaoXadrez('f', 1).ConverterPosicao());
            // tab.ColocarPeca(new Cavalo(tab, Cor.Branca), new PosicaoXadrez('g', 1).ConverterPosicao());
            // tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('h', 1).ConverterPosicao());

            // for (int i = Convert.ToInt32('a'); i < Convert.ToInt32('a') + 8; i++)
            // {
            //     tab.ColocarPeca(new Peao(tab, Cor.Branca), new PosicaoXadrez(Convert.ToChar(i), 2).ConverterPosicao());
            // }
        }
    }
}
