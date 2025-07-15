using System.Windows;

namespace FeedbackHub.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int rating = (int)RatingSlider.Value;
            string note = NoteTextBox.Text;

            ResultTextBlock.Text = $"Оценка: {rating}\nЗаметка: {note}";
        }
    }
}