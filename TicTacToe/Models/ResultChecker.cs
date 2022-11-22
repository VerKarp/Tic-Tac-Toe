using TicTacToe.Models.Enums;

namespace TicTacToe.Models
{
    public static class ResultChecker
    {
        private static GameBoard gameBoard;
        private static CellState WinnerState;

        #region VICTORYCHECKS

        internal static CellState CheckWin(GameBoard board)
        {
            gameBoard = board;

            WinnerState = CellState.NotPressed;

            if (CheckVertical() || CheckHorizontal() ||
                CheckLeftDiagonal() || CheckRightDiagonal()
                || CheckTie())
                return WinnerState;

            return WinnerState;
        }

        private static bool CheckVertical()
        {
            for (int i = 0; i < gameBoard.Size; i++)
            {
                int count = 0;

                for (int j = 0; j < gameBoard.Size - 1; j++)
                    if (gameBoard.Cells[j][i].State != CellState.NotPressed &&
                        gameBoard.Cells[j][i].State == gameBoard.Cells[j + 1][i].State)
                    {
                        count++;
                    }

                if (count == gameBoard.Size - 1)
                {
                    WinnerState = gameBoard.Cells[0][i].State;

                    for (int j = 0; j < gameBoard.Size; j++)
                        gameBoard.Cells[j][i].Winner = true;

                    return true;
                }
            }

            return false;
        }

        private static bool CheckHorizontal()
        {
            for (int i = 0; i < gameBoard.Size; i++)
            {
                int count = 0;

                for (int j = 0; j < gameBoard.Size - 1; j++)
                {
                    if (gameBoard.Cells[i][j].State != CellState.NotPressed &&
                        gameBoard.Cells[i][j].State == gameBoard.Cells[i][j + 1].State)
                    {
                        count++;
                    }
                }

                if (count == gameBoard.Size - 1)
                {
                    WinnerState = gameBoard.Cells[i][0].State;

                    for (int j = 0; j < gameBoard.Size; j++)
                        gameBoard.Cells[i][j].Winner = true;

                    return true;
                }
            }

            return false;
        }

        private static bool CheckLeftDiagonal()
        {
            int count = 0;

            for (int i = 0; i < gameBoard.Size - 1; i++)
            {
                for (int j = 0; j < gameBoard.Size - 1; j++)
                {
                    if (i == j)
                    {
                        if (gameBoard.Cells[i][j].State != CellState.NotPressed &&
                            gameBoard.Cells[i][j].State == gameBoard.Cells[i + 1][j + 1].State)
                            count++;
                    }
                }
            }

            if (count == gameBoard.Size - 1)
            {
                WinnerState = gameBoard.Cells[0][0].State;

                for (int i = 0; i < gameBoard.Size; i++)
                {
                    for (int j = 0; j < gameBoard.Size; j++)
                    {
                        if (i == j)
                        {
                            gameBoard.Cells[i][j].Winner = true;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        private static bool CheckRightDiagonal()
        {
            int count = 0;

            for (int i = 0; i < gameBoard.Size - 1; i++)
            {
                for (int j = gameBoard.Size - i - 1; j >= gameBoard.Size - i - 1; j--)
                {
                    if (gameBoard.Cells[i][j].State != CellState.NotPressed &&
                        gameBoard.Cells[i][j].State == gameBoard.Cells[i + 1][j - 1].State)
                        count++;
                }
            }

            if (count == gameBoard.Size - 1)
            {
                WinnerState = gameBoard.Cells[0][gameBoard.Size - 1].State;

                for (int i = 0; i < gameBoard.Size - 1; i++)
                {
                    for (int j = gameBoard.Size - i - 1; j >= gameBoard.Size - i - 1; j--)
                    {
                        gameBoard.Cells[i][j].Winner = true;
                        gameBoard.Cells[i + 1][j - 1].Winner = true;
                    }
                }

                return true;
            }

            return false;
        }

        private static bool CheckTie()
        {
            int count = 0;

            for (int i = 0; i < gameBoard.Size; i++)
            {
                for (int j = 0; j < gameBoard.Size; j++)
                {
                    if (gameBoard.Cells[i][j].State != CellState.NotPressed)
                        count++;
                }
            }

            if (count == gameBoard.Size * gameBoard.Size)
            {
                WinnerState = CellState.Empty;
                return true;
            }

            return false;
        }

        #endregion
    }
}