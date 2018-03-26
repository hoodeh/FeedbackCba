
namespace FeedbackCba.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
        public bool IsActive { get; set; }
    }
}