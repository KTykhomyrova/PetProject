using FeedbackHub.ViewModel;
using System.Windows;

namespace FeedbackHub.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}