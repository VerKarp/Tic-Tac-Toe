using TicTacToe.Services;
using TicTacToe.Services.Interfaces;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties

        #region Title

        private string _title = "Tic-Tac-Toe";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        public GameFieldViewModel GameFieldViewModel { get; set; }

        #endregion

        public MainWindowViewModel()
        {
            IWindowDialogService windowDialogService = new WindowDialogService();
            GameFieldViewModel = new(windowDialogService);
        }
    }
}