using System;
using System.Windows.Input;
using TicTacToe.Models.Enums;
using TicTacToe.Models;
using TicTacToe.Infrastructure.Commands;
using TicTacToe.ViewModels.Base;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.ViewModels
{
    internal class GameFieldViewModel : ViewModel
    {
        #region Fields

        private GameMode _gameMode = GameMode.Player;

        private CellState _turn = CellState.Cross;

        private readonly IWindowDialogService _dialog;

        #endregion

        #region Properties

        #region Title

        private string _title = "Tic-Tac-Toe";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region GameBoard - Board

        private GameBoard _gameBoard = new(3);
        public GameBoard Board
        {
            get => _gameBoard;
            set => Set(ref _gameBoard, value);
        }

        #endregion

        #region int - Depth

        public int Depth { get; set; }

        #endregion

        #region GameStatus Status

        private GameStatus _gameStatus = GameStatus.GameOn;
        public GameStatus Status
        {
            get => _gameStatus;
            set => Set(ref _gameStatus, value);
        }

        #endregion

        #endregion

        #region Commands

        #region NewGameCommand

        public ICommand NewGameCommand { get; }
        private void OnNewGameCommandExecute(object p)
        {
            Board = new(Board.Size);
            Status = GameStatus.GameOn;
        }

        #endregion

        #region ChangeGameBoardSizeCommand

        public ICommand ChangeGameBoardSizeCommand { get; }
        private void OnChangeGameBoardSizeCommandExecute(object p) =>
            NewGameCommand.Execute(Board.Size = Convert.ToInt32(p));

        #endregion

        #region ChangeGameModeCommand

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

        #endregion

        #region GameBoardClickCommand

        public ICommand GameBoardClickCommand { get; }
        private void OnGameBoardClickCommandExecute(object p)
        {
            if (p is Cell cell)
            {
                cell.State = _turn;

                if (_gameMode == GameMode.Player)
                    _turn = _turn == CellState.Cross ? CellState.Zero : CellState.Cross;

                if (_gameMode == GameMode.EasyBot || _gameMode == GameMode.HardBot)
                    MinimaxBot.Bot(Board, CellState.Zero, Depth);

                CheckWin();
            }
        }

        private bool CanGameBoardClickCommandExecuted(object p) =>
            p is Cell cell && cell.State == CellState.NotPressed;

        #endregion

        #endregion

        private void CheckWin()
        {
            CellState win = ResultChecker.CheckWin(Board);

            if (win != CellState.NotPressed)
            {
                switch (win)
                {
                    case CellState.Zero:
                        _dialog.ShowInformation("TAC!", "Game result");
                        Status = GameStatus.TacWon;
                        break;
                    case CellState.Cross:
                        _dialog.ShowInformation("TIC!", "Game result");
                        Status = GameStatus.TicWon;
                        break;
                    case CellState.Empty:
                        _dialog.ShowInformation("Toe...", "Game result");
                        Status = GameStatus.Toe;
                        break;
                    default:
                        Status = GameStatus.GameOff;
                        break;
                }

                Status = GameStatus.GameOff;
                _turn = CellState.Cross;
            }
        }

        public GameFieldViewModel(IWindowDialogService windowDialogService)
        {
            _dialog = windowDialogService;

            #region Commands

            NewGameCommand = new LambdaCommand(OnNewGameCommandExecute);
            GameBoardClickCommand = new LambdaCommand(OnGameBoardClickCommandExecute, CanGameBoardClickCommandExecuted);
            ChangeGameBoardSizeCommand = new LambdaCommand(OnChangeGameBoardSizeCommandExecute);
            ChangeGameModeCommand = new LambdaCommand(OnChangeGameModeCommandExecute);

            #endregion
        }
    }
}