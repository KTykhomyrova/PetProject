using FeedbackHub.Model.DbContexts;
using FeedbackHub.View;
using FeedbackHub.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace FeedbackHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Register your services here
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<ApplicationDbContext>();

            // todo: add logging service

            // Example: Register other services
            // services.AddSingleton<IYourService, YourService>();
            // services.AddScoped<IYourService, YourService>();
            // services.AddTransient<IYourService, YourService>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                await _host.StartAsync();


                var mainWindow = _host.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();

            }
            catch (Exception ex)
            {
                // todo: add loging
                throw;
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
