using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedbackHub.Model.DbContexts;
using FeedbackHub.Model.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FeedbackHub.ViewModel
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _rating = 5;

        [ObservableProperty]
        private string _note = string.Empty;

        [ObservableProperty]
        private string _result = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Feedback> _feedbacks;

        private readonly ApplicationDbContext _context = new();

        public MainWindowViewModel()
        {
            _feedbacks = new(_context.Feedbacks.ToList());

            FillDesignDataIfNeed();
        }

        [RelayCommand]
        private void Create()
        {
            Note = Note.Trim();
            Result = $"Оценка: {Rating}\nЗаметка: {Note}";

            var feedback = new Feedback() { Note = Note, Rating = Rating };

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            Feedbacks.Add(feedback);
        }

        [RelayCommand]
        private void Modify(Feedback feedback)
        {
            var old = _context.Feedbacks.Find(feedback.Id);

            if (old == null)
            {
                return;
            }

           // old.Rating = feedback.Rating;
            //old.Note = feedback.Note;

            _context.SaveChanges();

            feedback.ModifiedAt = old.ModifiedAt;

            OnPropertyChanged(nameof(Feedbacks));
        }

        [RelayCommand]
        private void Delete(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);
            _context.SaveChanges();

            Feedbacks.Remove(feedback);
        }

        private void FillDesignDataIfNeed()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            Rating = 9;
            Note = "Пример заметки";
            Result = $"Оценка: {Rating}\nЗаметка: {Note}";
            Feedbacks =
                [
                    new() { Rating = 2, Note = "Пример отзыва 1", CreatedAt = DateTime.UtcNow },
                    new() { Rating = 8, Note = "Пример отзыва 2", CreatedAt = DateTime.UtcNow }
                ];
        }
    }
}
