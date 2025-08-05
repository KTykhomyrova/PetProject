using CommunityToolkit.Mvvm.ComponentModel;
using FeedbackHub.Model.Entities;

namespace FeedbackHub.ViewModel.Wrappers
{
    public partial class FeedbackWrapper : ObservableObject
    {
        [ObservableProperty] private int _id;
        [ObservableProperty] private int _rating;
        [ObservableProperty] private string? _note;
        [ObservableProperty] private DateTime? _createdAt;
        [ObservableProperty] private DateTime? _modifiedAt;

        public FeedbackWrapper()
        {

        }

        public FeedbackWrapper(Feedback feedback)
        {
            Id = feedback.Id;
            Rating = feedback.Rating;
            Note = feedback.Note;
            CreatedAt = feedback.CreatedAt;
            ModifiedAt = feedback.ModifiedAt;
        }

        public Feedback ConvertToModel() => new()
        {
            Id = Id,
            Rating = Rating,
            Note = Note,
            CreatedAt = CreatedAt,
            ModifiedAt = ModifiedAt
        };
    }
}
