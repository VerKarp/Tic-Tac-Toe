using TicTacToe.ViewModels.Base;

namespace TicTacToe.ViewModels
{
    public class AuthorizationViewModel : ViewModel
    {
		private string _login;
		public string Login
		{
			get => _login;
			set => Set(ref _login, value);
		}

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

    }
}