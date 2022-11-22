using System;
using TicTacToe.Services.Interfaces;
using TicTacToe.Stores;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.Services
{
    public class NavigationService<TViewModel> : INavigationService 
        where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate() => _navigationStore.CurrentViewModel = _createViewModel();
    }
}