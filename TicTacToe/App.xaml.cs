using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services;
using TicTacToe.Views.Windows;
using TicTacToe.Stores;
using TicTacToe.ViewModels;

namespace TicTacToe
{
    public partial class App : Application 
    { 
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<NavigationStore>(s => new());

                    services.AddTransient<IWindowDialogService, WindowDialogService>();

                    services.AddSingleton<INavigationService>(s => CreateGameFieldViewModelService());

                    services.AddTransient<GameFieldViewModel>(s => 
                        new(s.GetRequiredService<WindowDialogService>()));

                    services.AddTransient<AuthorizationViewModel>(s => new());

                    services.AddTransient<AboutViewModel>(s => new());
                    services.AddTransient<SettingsViewModel>(s => new());

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>(s => new()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });

                })
                .Build();
        }

        private INavigationService CreateNavigationBarViewModelService() =>
            new NavigationService<NavigationBarViewModel>(
                AppHost.Services.GetRequiredService<NavigationStore>(),
                () => CreateNavigationBarViewModel());

        private INavigationService CreateAboutService() =>
            new LayoutNavigationService<AboutViewModel>(
                AppHost.Services.GetRequiredService<NavigationStore>(),
                () => AppHost.Services.GetRequiredService<AboutViewModel>(),
                () => CreateNavigationBarViewModel());

        private INavigationService CreateSettingsService() =>
            new LayoutNavigationService<SettingsViewModel>(
                AppHost.Services.GetRequiredService<NavigationStore>(),
                () => AppHost.Services.GetRequiredService<SettingsViewModel>(),
                () => CreateNavigationBarViewModel());

        private INavigationService CreateGameFieldViewModelService() =>
            new LayoutNavigationService<GameFieldViewModel>(
                AppHost.Services.GetRequiredService<NavigationStore>(),
                () => CreateGameFieldViewModel(),
                () => CreateNavigationBarViewModel());

        private INavigationService CreateAuthorizationViewModelService() =>
            new LayoutNavigationService<AuthorizationViewModel>(
                AppHost.Services.GetRequiredService<NavigationStore>(),
                () => new AuthorizationViewModel(),
                () => CreateNavigationBarViewModel());

        private NavigationBarViewModel CreateNavigationBarViewModel() =>
            new NavigationBarViewModel(
                CreateGameFieldViewModelService(),
                CreateAuthorizationViewModelService(),
                CreateAboutService(),
                CreateSettingsService());

        private GameFieldViewModel CreateGameFieldViewModel() =>
            new GameFieldViewModel(AppHost.Services.GetRequiredService<IWindowDialogService>());

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            AppHost.Services.GetRequiredService<INavigationService>().Navigate();
            MainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}