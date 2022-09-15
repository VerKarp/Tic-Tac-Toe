using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TicTacToe.Infrastructure.Commands;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
		private string _title = "Tic-Tac-Toe";

		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

		#region Commands

		public ICommand CloseApplicationCommand { get; }
		private void OnCloseApplicationCommandExecute(object p) => Application.Current.Shutdown();

		#endregion

		public MainWindowViewModel()
		{
			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute);
		}

    }
}