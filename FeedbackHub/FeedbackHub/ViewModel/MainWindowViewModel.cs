using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedbackHub.Model.DbContexts;
using FeedbackHub.Model.Entities;

namespace FeedbackHub.ViewModel
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        private int _rating = 10;
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 0 || value > 10)
                    return;

                SetProperty(ref _rating, value);
                OnPropertyChanged(nameof(Rating));
            }
        }

        private string _note = string.Empty;
        public string Note
        {
            get => _note;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                SetProperty(ref _note, value);
                OnPropertyChanged(nameof(Note));
            }
        }

        private string _result = string.Empty;
        public string Result
        {
            get => _result;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                SetProperty(ref _result, value);
                OnPropertyChanged(nameof(Result));
            }
        }

        [RelayCommand]
        private void Save()
        {
            Note = Note.Trim();
            Result = $"Оценка: {Rating}\nЗаметка: {Note}";

            using (var db = new ApplicationDbContext())
            {
                var feedback = new Feedback() { Note = Note, Rating = Rating };

                db.Add(feedback);
                db.SaveChanges();
            }
        }
    }
}
