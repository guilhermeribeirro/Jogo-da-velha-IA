using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_da_velha
{
    public class JogoMinimax
    {
        
        public char jogadaIA = 'X';
        public char jogadaJogador = 'O';
       public char[,] tabuleiro = new char[3, 3];
        public JogoMinimax()
        {
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tabuleiro[i, j] = ' ';
                }
            }
        }

        public void Jogar()
        {
            while (true)
            {
                ExibirTabuleiro();
                JogadaJogador();
                if (VerificaGanhadorJogo(jogadaJogador))
                {
                    Console.WriteLine("Parabéns, você venceu !");
                    break;
                }

                if (TabuleiroCheio())
                {
                    Console.WriteLine("Velha!");
                    break;
                }

                JogadaComputador();
                if (VerificaGanhadorJogo(jogadaIA))
                {
                    Console.WriteLine("A IA venceu!, hahaha");
                    break;
                }

                if (TabuleiroCheio())
                {
                    Console.WriteLine("Velha!");
                    break;
                }
            }

            
            ExibirTabuleiroFinal();
        }



        private void ExibirTabuleiro()
        {
            Console.WriteLine("Tabuleiro:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tabuleiro[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("-----");
            }
        }

        private void ExibirTabuleiroFinal()
        {
            Console.WriteLine("Tabuleiro:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(tabuleiro[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("-----");
            }
        }

        private void JogadaJogador()
        {
            int linha, coluna;
            do
            {
                Console.WriteLine("Você é bolinha( O ), Escolha entre a linha (1, 2, 3) e a coluna (1, 2, 3) para marcar a jogada:");
                linha = int.Parse(Console.ReadLine()) - 1;  
                coluna = int.Parse(Console.ReadLine()) - 1; 
            } while (tabuleiro[linha, coluna] != ' ');

            tabuleiro[linha, coluna] = jogadaJogador;
        }


        public void JogadaComputador()
        {
            int melhorPontuacao = int.MinValue;
            int melhorLinha = -1;
            int melhorColuna = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        tabuleiro[i, j] = jogadaIA;
                        int pontuacao = Minimax(tabuleiro, false);
                        tabuleiro[i, j] = ' ';

                        if (pontuacao > melhorPontuacao)
                        {
                            melhorPontuacao = pontuacao;
                            melhorLinha = i;
                            melhorColuna = j;
                        }
                    }
                }
            }

            tabuleiro[melhorLinha, melhorColuna] = jogadaIA;
        }

        private int Minimax(char[,] tabuleiro, bool maximizing)
        {
            if (VerificaGanhadorJogo(jogadaIA))
                return 10;
            if (VerificaGanhadorJogo(jogadaJogador))
                return -10;
            if (TabuleiroCheio())
                return 0;

            if (maximizing)
            {
                int melhorPontuacao = int.MinValue;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tabuleiro[i, j] == ' ')
                        {
                            tabuleiro[i, j] = jogadaIA;
                            int pontuacao = Minimax(tabuleiro, false);
                            tabuleiro[i, j] = ' ';
                            melhorPontuacao = Math.Max(pontuacao, melhorPontuacao);
                        }
                    }
                }

                return melhorPontuacao;
            }
            else
            {
                int melhorPontuacao = int.MaxValue;

                for (int i = 0; i < 3; i++ ) 
                {
                    for (int j = 0; j < 3; j++  )
                    {
                        if (tabuleiro[i, j] == ' ')
                        {
                            tabuleiro[i, j] = jogadaJogador;
                            int pontuacao = Minimax(tabuleiro, true);
                            tabuleiro[i, j] = ' ';
                            melhorPontuacao = Math.Min(pontuacao, melhorPontuacao);
                        }
                    }
                }

                return melhorPontuacao;
            }
        }

        private bool VerificaGanhadorJogo(char jogador)
        {
            
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                {
                    return true;
                }
            }

            
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[0, j] == jogador && tabuleiro[1, j] == jogador && tabuleiro[2, j] == jogador)
                {
                    return true;
                }
            }

            
            if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            {
                return true;
            }

            
            if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            {
                return true;
            }

            
            return false;
        }

        private bool TabuleiroCheio()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
