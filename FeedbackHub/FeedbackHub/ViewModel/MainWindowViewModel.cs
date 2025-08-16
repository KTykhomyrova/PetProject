using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FeedbackHub.Model.DbContexts;
using FeedbackHub.Model.Entities;
using FeedbackHub.Services;
using FeedbackHub.ViewModel.Wrappers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace FeedbackHub.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        #region ObservableProperties

        [ObservableProperty]
        int _rating = 5;

        [ObservableProperty]
        string _note = string.Empty;

        [ObservableProperty]
        string _result = string.Empty;

        [ObservableProperty]
        ObservableCollection<FeedbackWrapper> _feedbacks;

        #endregion

        #region OnPropertyChanged Methods

        partial void OnRatingChanged(int value) =>
            Result = GetResultMessage(Rating, Note);

        partial void OnNoteChanged(string value) =>
            Result = GetResultMessage(Rating, Note);

        #endregion

        #region Services

        readonly ILoggingService _loggingService;
        readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public MainWindowViewModel(ILoggingService loggingService, ApplicationDbContext context)
        {
            _loggingService = loggingService;
            _context = context;

            var feedbackWrappers = _context.Feedbacks.Select(x => new FeedbackWrapper(x));
            _feedbacks = new(feedbackWrappers);

            FillDesignDataIfNeed();
        }

        #endregion

        #region RelayCommands

        [RelayCommand]
        private void Create()
        {
            var feedback = new Feedback() { Note = Note, Rating = Rating };

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            Feedbacks.Add(new FeedbackWrapper(feedback));
        }

        [RelayCommand]
        private void Modify(FeedbackWrapper feedback)
        {
            var tracked = _context.Feedbacks.Find(feedback.Id);
            if (tracked is null)
                return;

            tracked.Rating = feedback.Rating;
            tracked.Note = feedback.Note;

            _context.SaveChanges();

            feedback.ModifiedAt = tracked.ModifiedAt;
        }

        [RelayCommand]
        private void Delete(FeedbackWrapper feedback)
        {
            var tracked = _context.Feedbacks.Find(feedback.Id);
            if (tracked is null)
                return;

            _context.Feedbacks.Remove(tracked);
            _context.SaveChanges();

            Feedbacks.Remove(feedback);
        }

        #endregion

        #region Private Methods

        private void FillDesignDataIfNeed()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return;
            }

            Rating = 9;
            Note = "Пример заметки";
            Result = GetResultMessage(Rating, Note);
            Feedbacks =
                [
                    new() { Rating = 2, Note = "Пример отзыва 1", CreatedAt = DateTime.UtcNow },
                    new() { Rating = 8, Note = "Пример отзыва 2", CreatedAt = DateTime.UtcNow }
                ];
        }

        private static string GetResultMessage(int rating, string note) =>
            $"Оценка: {rating} \nЗаметка: {note}";

        #endregion
    }
}