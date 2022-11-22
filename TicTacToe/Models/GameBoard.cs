using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class GameBoard
    {
        public List<List<Cell>> Cells { get; set; }

        private int _size;
        public int Size 
        {
            get => _size;
            set
            {
                if (value > 5 || value < 3)
                    _size = 3;

                _size = value;
            }
        }

        public GameBoard(int size)
        {
            Size = size;

            List<List<Cell>> arena = new();

            for (int i = 0; i < size; i++)
            {
                List<Cell> row = new();
                for (int j = 0; j < size; j++)
                {
                    row.Add(new Cell());
                }
                arena.Add(row);
            }

            Cells = arena;
        }

        public GameBoard() { }
    }
}