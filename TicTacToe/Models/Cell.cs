using TicTacToe.Models.Enums;

namespace TicTacToe.Models
{
    internal class Cell
    {
        public CellState State { get; set; }
        public bool Winner { get; set; }

        public Cell()
        {
            State = CellState.NotPressed;
            Winner = false;
        }
    }
}