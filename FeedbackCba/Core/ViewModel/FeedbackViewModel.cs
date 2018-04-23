using System;
using System.Collections.Generic;
using FeedbackCba.Core.Models;

namespace FeedbackCba.Core.ViewModel
{
    public class FeedbackViewModel
    {
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public bool IsMainPage { get; set; }
        public string PageUrl { get; set; }
        public string Statement { get; set; }
        public string MainQuestion { get; set; }
        public decimal Rate { get; set; }
        public DateTime SubmitDate { get; set; }
        public int QuestionId { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public string UserReply { get; set; }
        public string BgColor { get; set; }
    }
}