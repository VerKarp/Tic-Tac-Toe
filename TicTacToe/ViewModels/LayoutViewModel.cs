using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    public class LayoutViewModel : ViewModel
    {
        public LayoutViewModel(ViewModel contentViewModel, NavigationBarViewModel navigationBarViewModel)
        {
            ContentViewModel = contentViewModel;
            NavigationBarViewModel = navigationBarViewModel;
        }

        public ViewModel ContentViewModel { get; }
        public NavigationBarViewModel NavigationBarViewModel { get; }

    }
}