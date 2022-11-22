using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TicTacToe.Services.Interfaces;
using TicTacToe.Services;
using TicTacToe.Views.Windows;
using TicTacToe.Stores;
using TicTacToe.ViewModels;
using System;

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
                    services.AddSingleton<NavigationStore>();
                    services.AddTransient<IWindowDialogService, WindowDialogService>();

                    services.AddSingleton<INavigationService>(s =>
                        new NavigationService<GameFieldViewModel>(s.GetRequiredService<NavigationStore>(),
                            () => new GameFieldViewModel(s.GetRequiredService<IWindowDialogService>())));

                    services.AddTransient<GameFieldViewModel>(s => 
                        new(s.GetRequiredService<WindowDialogService>()));

                    services.AddTransient<AuthorizationViewModel>(s => new());

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>(s => new()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });

                })
                .Build();
        }

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