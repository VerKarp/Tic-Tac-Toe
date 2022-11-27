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
                    services.AddSingleton<NavigationStore>(s => new()
                    {
                        CurrentViewModel = s.GetRequiredService<LayoutViewModel>()
                    });

                    services.AddTransient<IWindowDialogService, WindowDialogService>();

                    services.AddSingleton<INavigationService>(s =>
                        new NavigationService<GameFieldViewModel>(s.GetRequiredService<NavigationStore>(),
                            () => new GameFieldViewModel(s.GetRequiredService<IWindowDialogService>())));

                    services.AddTransient<GameFieldViewModel>(s => 
                        new(s.GetRequiredService<WindowDialogService>()));

                    services.AddTransient<AuthorizationViewModel>(s => new());

                    services.AddTransient<NavigationBarViewModel>(s => new());

                    services.AddTransient<LayoutViewModel>(s => new(s.GetRequiredService<AuthorizationViewModel>(),
                        s.GetRequiredService<NavigationBarViewModel>()));

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>(s => new()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });

                })
                .Build();
        }

        private INavigationService CreateLayoutViewModelService() => 
            new NavigationService<LayoutViewModel>(
                AppHost.Services.GetRequiredService<NavigationStore>(),
                () => CreateLayoutViewModel());

        private LayoutViewModel CreateLayoutViewModel() =>
            new LayoutViewModel(
                AppHost.Services.GetRequiredService<AuthorizationViewModel>(),
                AppHost.Services.GetRequiredService<NavigationBarViewModel>());

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