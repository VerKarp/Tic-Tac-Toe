using System;
using TicTacToe.Services.Interfaces;
using TicTacToe.Stores;
using TicTacToe.ViewModels;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.Services
{
    public class LayoutNavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;

        public LayoutNavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel,
            Func<NavigationBarViewModel> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        public void Navigate() =>
            _navigationStore.CurrentViewModel = 
            new LayoutViewModel(_createViewModel(), _createNavigationBarViewModel());
    }
}