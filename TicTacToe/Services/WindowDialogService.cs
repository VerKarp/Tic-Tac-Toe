using System.Windows;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Services
{
    internal class WindowDialogService : IWindowDialogService
    {
        public void ShowInformation(string Information, string Caption = "Результат игры") =>
            MessageBox.Show(
                Information,
                Caption,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
    }
}