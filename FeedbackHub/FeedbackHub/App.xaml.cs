using FeedbackHub.View;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FeedbackHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var mainWindow = new MainWindow();
                mainWindow.Show();
                
            }
            catch (Exception ex)
            {
                // todo: add login
                throw;
            }
        }
    }
}
