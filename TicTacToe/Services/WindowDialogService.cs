using System.Windows;
using TicTacToe.Services.Interfaces;

namespace TicTacToe.Services
{
    public class WindowDialogService : IWindowDialogService
    {
        public void ShowInformation(string Information, string Caption = "Game result") =>
            MessageBox.Show(
                Information,
                Caption,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
    }
}