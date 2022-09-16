using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicTacToe.Infrastructure.Commands;
using TicTacToe.Models;
using TicTacToe.Models.Enums;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
		private void OnNewGameCommandExecute(object p) => Board = new(3);

		#endregion

		private GameBoard _gameBoard;
		public GameBoard Board
		{
			get => _gameBoard;
			set => Set(ref _gameBoard, value);
		}


		public MainWindowViewModel()
		{
            Board = new(3);

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute);
			NewGameCommand = new LambdaCommand(OnNewGameCommandExecute);
		}

    }
}