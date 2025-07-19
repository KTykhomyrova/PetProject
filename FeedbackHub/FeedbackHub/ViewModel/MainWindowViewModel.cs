using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedbackHub.Model.DbContexts;
using FeedbackHub.Model.Entities;
using System.Collections.ObjectModel;

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

        [ObservableProperty]
        private ObservableCollection<Feedback> _feedbacks;

        private readonly  ApplicationDbContext _context;

        public MainWindowViewModel()
        {
            _context = new ApplicationDbContext();
            _feedbacks = new ObservableCollection<Feedback>(_context.Feedbacks.ToList());
        }

        [RelayCommand]
        private void Save()
        {
            Note = Note.Trim();
            Result = $"Оценка: {Rating}\nЗаметка: {Note}";

            var feedback = new Feedback() { Note = Note, Rating = Rating };

            _context.Add(feedback);
            _context.SaveChanges();

            Feedbacks.Add(feedback);
        }

        [RelayCommand]
        private void Delete(Feedback feedback)
        {
            _context.Remove(feedback);
            _context.SaveChanges();

            Feedbacks.Remove(feedback);
        }
    }
}
