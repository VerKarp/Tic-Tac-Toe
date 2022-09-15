using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	}
}