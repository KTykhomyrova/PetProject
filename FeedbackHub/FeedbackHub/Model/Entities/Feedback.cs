using FeedbackHub.Model.Contracts;

namespace FeedbackHub.Model.Entities
{
    public class Feedback : IHasCreatedAt, IHasModifiedAt
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
