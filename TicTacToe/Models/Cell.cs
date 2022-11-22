using TicTacToe.Models.Enums;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.Models
{
    public class Cell : ViewModel
    {
        private CellState _state;
        public CellState State 
        { 
            get => _state; 
            set => Set(ref _state, value); 
        }

        private bool _winner;
        public bool Winner 
        { 
            get => _winner; 
            set => Set(ref _winner, value); 
        }

        public Cell()
        {
            State = CellState.NotPressed;
            Winner = false;
        }
    }
}