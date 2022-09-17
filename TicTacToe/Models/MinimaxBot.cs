using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models.Enums;

namespace TicTacToe.Models
{
    internal static class MinimaxBot
    {
        private static int X;
        private static int Y;

        private static GameBoard CloneBoard(GameBoard board)
        {
            List<List<Cell>> clone = new();

            for (int i = 0; i < board.Size; i++)
            {
                List<Cell> row = new();

                for (int j = 0; j < board.Size; j++)
                    row.Add(new Cell());

                clone.Add(row);
            }

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    clone[i][j].State = board.Cells[i][j].State;
                }
            }

            return new GameBoard() { Cells = clone, Size = board.Size };
        }

        private static GameBoard MakeBoardMove(GameBoard board, CellState state, int x, int y)
        {
            List<List<Cell>> newBoard = board.Cells;
            newBoard[x][y].State = state;
            return new GameBoard() { Cells = newBoard, Size = board.Size};
        }

        private static CellState SwitchState(CellState state)
        {
            if (state == CellState.Cross) return CellState.Zero;
            else return CellState.Cross;
        }

        private static int CheckScore(GameBoard board, CellState state)
        {
            if (ResultChecker.CheckWin(board) == state) return 1;

            else if (ResultChecker.CheckWin(board) == SwitchState(state)) return -1;

            else return 0;
        }

        private static bool TieWin(GameBoard board)
        {
            int count = 0;

            for (int i = 0; i < board.Size; i++)
                for (int j = 0; j < board.Size; j++)
                    if (board.Cells[i][j].State != CellState.NotPressed)
                        count++;

            if (count == board.Size * board.Size)
                return true;

            return false;
        }

        private static float Minimax(GameBoard gameBoard, CellState state, int depth)
        {
            GameBoard board = CloneBoard(gameBoard);

            if (CheckScore(board, CellState.Zero) != 0)
                return CheckScore(board, CellState.Zero);
            else if (TieWin(board) || depth == 0) return 0;

            List<float> scores = new();
            List<int> x = new();
            List<int> y = new();

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    if (board.Cells[i][j].State == CellState.NotPressed)
                    {
                        scores.Add(Minimax(MakeBoardMove(board, state, i, j), SwitchState(state), depth - 1));
                        x.Add(i);
                        y.Add(j);
                        board.Cells[i][j].State = CellState.NotPressed;
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

        public static void Bot(GameBoard board, CellState state, int depth)
        {
            _ = Minimax(board, state, depth);

            board.Cells[X][Y].State = CellState.Zero;
        }
    }
}