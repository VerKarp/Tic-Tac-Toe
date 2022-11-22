using TicTacToe.Stores;
using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly NavigationStore _navigationStore;

        #region Properties

        #region Title

        private string _title = "Tic-Tac-Toe";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        #endregion

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged() => OnPropertyChanged(nameof(CurrentViewModel));
    }
}