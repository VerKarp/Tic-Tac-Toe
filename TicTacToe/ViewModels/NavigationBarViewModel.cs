using System.Windows.Input;
using TicTacToe.Infrastructure.Commands;
using TicTacToe.Services.Interfaces;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    public class NavigationBarViewModel : ViewModel
    {
        public ICommand GameNavigateCommand { get; }
        public ICommand AuthorizationNavigateCommand { get; }

        public NavigationBarViewModel(INavigationService gameFieldNavigationService,
            INavigationService authorizationNavigationService)
        {
            GameNavigateCommand = new NavigateCommand(gameFieldNavigationService);
            AuthorizationNavigateCommand = new NavigateCommand(authorizationNavigationService);
        }
    }
}