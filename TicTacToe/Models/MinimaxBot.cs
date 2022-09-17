using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.Enums;

namespace TicTacToe.Models
{
    internal static class MinimaxBot
    {
        public static int X { get; set; }
        public static int Y { get; set; }

        private static List<List<Cell>> CloneBoard(List<List<Cell>> board, int size)
        {
            List<List<Cell>> clone = new();

            for (int i = 0; i < size; i++)
            {
                List<Cell> row = new();

                for (int j = 0; j < size; j++)
                    row.Add(new Cell());

                clone.Add(row);
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    clone[i][j].State = board[i][j].State;
                }
            }

            return clone;
        }

        private static List<List<Cell>> MakeBoardMove(List<List<Cell>> board, CellState state, int x, int y)
        {
            List<List<Cell>> newBoard = board;
            newBoard[x][y].State = state;
            return newBoard;
        }

        private static CellState SwitchState(CellState state)
        {
            if (state == CellState.Cross) return CellState.Zero;
            else return CellState.Cross;
        }

        private static int CheckScore(List<List<Cell>> cells, int size, CellState state)
        {
            if (ResultChecker.CheckWin(new GameBoard(){ Cells = cells, Size = size }) == state) return 1;

            else if (ResultChecker.CheckWin(new GameBoard() { Cells = cells, Size = size }) 
                == SwitchState(state)) return -1;

            else return 0;
        }

        private static float Minimax(List<List<Cell>> cells, int size, CellState state, int depth)
        {
            List<List<Cell>> board = CloneBoard(cells, size);

            if (CheckScore(board, size, CellState.Zero) != 0)
                return CheckScore(board, size, CellState.Zero);
            else if (TieWin(board, size) || depth == 0) return 0;

            List<float> scores = new();
            List<int> x = new();
            List<int> y = new();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i][j].State == CellState.NotPressed)
                    {
                        scores.Add(Minimax(MakeBoardMove(board, state, i, j), size, SwitchState(state), depth - 1));
                        x.Add(i);
                        y.Add(j);
                        board[i][j].State = CellState.NotPressed;
                    }

                }
            }

            if (state == CellState.Zero)
            {
                int maxScoreIndex = scores.IndexOf(scores.Max());
                X = x[maxScoreIndex];
                Y = y[maxScoreIndex];
                return scores.Max();
            }

            else
            {
                int minScoreIndex = scores.IndexOf(scores.Min());
                X = x[minScoreIndex];
                Y = y[minScoreIndex];
                return scores.Min();
            }
        }

        private static bool TieWin(List<List<Cell>> cells, int size)
        {
            int count = 0;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (cells[i][j].State != CellState.NotPressed)
                        count++;

            if (count == size * size)
                return true;

            return false;
        }

        public static void Bot(List<List<Cell>> cells, int size, CellState state, int depth)
        {
            _ = MinimaxBot.Minimax(cells, size, state, depth);

            cells[MinimaxBot.X][MinimaxBot.Y].State = CellState.Zero;
        }
    }
}