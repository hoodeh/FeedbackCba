namespace FeedbackCba.Core.Dtos
{
    public class FeedbackDto
    {
        public string UserId { get; set; }
        public bool IsMainPage { get; set; }
        public string PageUrl { get; set; }
        public decimal Rate { get; set; }
        public int QuestionId { get; set; }
        public string UserReply { get; set; }
    }
}