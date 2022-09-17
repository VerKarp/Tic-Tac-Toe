using System.Windows;
using TicTacToe.Infrastructure.Commands.Base;

namespace TicTacToe.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter) => Application.Current.Shutdown();
    }
}