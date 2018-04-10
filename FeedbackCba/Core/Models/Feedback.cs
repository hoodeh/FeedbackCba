using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackCba.Core.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [MaxLength(50)]
        public string UserId { get; set; }

        public DateTime SubmitDate { get; set; }
        public decimal Rate{ get; set; }
        public string PageUrl { get; set; }
        public bool IsMainPage { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string UserReply { get; set; }
    }
}