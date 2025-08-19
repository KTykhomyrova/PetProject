using FeedbackHub.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FeedbackHub.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            // Установка выделения для активной вкладки
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D7E9F9"));

            // Переключение контента
            switch (button.Tag.ToString())
            {
                case "Feedback":
                    FeedbackContent.Visibility = Visibility.Visible;
                    StatsContent.Visibility = Visibility.Collapsed;
                    break;

                case "Stats":
                    FeedbackContent.Visibility = Visibility.Collapsed;
                    StatsContent.Visibility = Visibility.Visible;
                    break;

                default:
                    return;
            }
        }
    }
}