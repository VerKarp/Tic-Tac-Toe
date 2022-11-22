using System;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.Stores
{
    public class NavigationStore
    {
		public event Action CurrentViewModelChanged;

		private ViewModel _currentViewModel;
		public ViewModel CurrentViewModel
        {
			get => _currentViewModel;
			set 
			{ 
				_currentViewModel = value;
				CurrentViewModelChanged?.Invoke();
			}
		}
	}
}