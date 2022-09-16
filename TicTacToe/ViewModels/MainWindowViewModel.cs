using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicTacToe.Infrastructure.Commands;
using TicTacToe.Models;
using TicTacToe.Models.Enums;
using TicTacToe.Services;
using TicTacToe.Services.Interfaces;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private GameMode _gameMode = GameMode.Player;

        WindowDialogService _dialog = new();

        private CellState _turn = CellState.Cross;

        private bool _isGameOn;
        public bool IsGameOn
        {
            get => _isGameOn;
            set
            {
                _isGameOn = value;
                OnPropertyChanged();
            }
        }

        #region Title

        private string _title = "Tic-Tac-Toe";

		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

        #endregion

        #region Commands

        public ICommand CloseApplicationCommand { get; }
		private void OnCloseApplicationCommandExecute(object p) => Application.Current.Shutdown();

		public ICommand NewGameCommand { get; }
		private void OnNewGameCommandExecute(object p) => Board = new(Board.Size);

        public ICommand ChangeGameBoardSizeCommand { get; }
        private void OnChangeGameBoardSizeCommandExecute(object p) => 
            NewGameCommand.Execute(Board.Size = Convert.ToInt32(p));

        public ICommand ChangeGameModeCommand { get; }
        private void OnChangeGameModeCommandExecute(object p)
        {
            switch (Board.Size)
            {
                case 3:
                    Depth = Convert.ToInt32(p) + 4;
                    break;

                case 4:
                    Depth = Convert.ToInt32(p) + 1;
                    break;

                default:
                    break;
            }
            
            NewGameCommand.Execute(_gameMode = (GameMode)Convert.ToInt32(p));
        }

        public ICommand GameBoardClickCommand { get; }
        private void OnGameBoardClickCommandExecute(object p)
        {
            if (p is Cell cell)
            {
                cell.State = _turn;

                if (_gameMode == GameMode.Player)
                    _turn = _turn == CellState.Cross ? CellState.Zero : CellState.Cross;

                if (_gameMode == GameMode.EasyBot || _gameMode == GameMode.MediumBot || _gameMode == GameMode.HardBot)
                    ResultChecker.Bot(Board.Cells, Board.Size, CellState.Zero, Depth);

                CheckWin();
            }
        }

        private bool CanGameBoardClickCommandExecuted(object p) =>
            p is Cell cell && cell.State == CellState.NotPressed;

        #endregion

        private GameBoard _gameBoard = new(3);
		public GameBoard Board
		{
			get => _gameBoard;
			set => Set(ref _gameBoard, value);
		}

        public int Depth { get; set; }

        private void CheckWin()
        {
            CellState win = ResultChecker.CheckWin(Board);

            if (win != CellState.NotPressed)
            {
                IsGameOn = false;

                if (win == CellState.Empty)
                    _dialog.ShowInformation("Toe...");

                if (win == CellState.Cross)
                    _dialog.ShowInformation("TIC!");

                if (win == CellState.Zero)
                    _dialog.ShowInformation("TAC!");

                _turn = CellState.Cross;
            }
        }

        public MainWindowViewModel()
		{
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute);
			NewGameCommand = new LambdaCommand(OnNewGameCommandExecute);
            GameBoardClickCommand = new LambdaCommand(OnGameBoardClickCommandExecute, CanGameBoardClickCommandExecuted);
            ChangeGameBoardSizeCommand = new LambdaCommand(OnChangeGameBoardSizeCommandExecute);
            ChangeGameModeCommand = new LambdaCommand(OnChangeGameModeCommandExecute);
        }
    }
}