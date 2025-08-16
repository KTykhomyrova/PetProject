using FeedbackHub.Model.DbContexts;
using FeedbackHub.Services;
using FeedbackHub.View;
using FeedbackHub.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;

namespace FeedbackHub
{
    public partial class App : Application
    {
        readonly IHost _host;
        readonly ILoggingService _logger;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();

            _logger = _host.Services.GetRequiredService<ILoggingService>();
            SubsicribeToUnhandledExceptions();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<ILoggingService>(provider =>
            {
                return new LoggingService("app.log");
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }

            base.OnExit(e);
        }

        private void SubsicribeToUnhandledExceptions()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.LogError("UI thread unhandled exception", e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            _logger.LogError("AppDomain unhandled exception", ex ?? new Exception("Unknown AppDomain exception"));
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger.LogError("Unobserved task exception", e.Exception);
            e.SetObserved();
        }
    }
}
