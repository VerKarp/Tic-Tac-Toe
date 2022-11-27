using TicTacToe.Infrastructure.Commands.Base;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Infrastructure.Commands
{
    class NavigateCommand : Command
    {
        private readonly INavigationService _navigationService;
        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) =>
            _navigationService.Navigate();
    }
}