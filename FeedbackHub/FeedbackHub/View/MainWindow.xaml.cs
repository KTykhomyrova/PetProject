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
            private void BurgerButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключение видимости меню
            if (MenuItemsPanel.Visibility == Visibility.Visible)
            {
                MenuItemsPanel.Visibility = Visibility.Collapsed;
                SideMenu.Width = 60;
                BurgerButton.Content = "☰";
            }
            else
            {
                MenuItemsPanel.Visibility = Visibility.Visible;
                SideMenu.Width = 200;
                BurgerButton.Content = "☰ Меню";
            }
        }

        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // Сброс стилей всех кнопок
                ResetTabButtons();

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
                }
            }
        }

        private void ResetTabButtons()
        {
            FeedbackTab.Background = Brushes.Transparent;
            StatsTab.Background = Brushes.Transparent;
        }
    }
}