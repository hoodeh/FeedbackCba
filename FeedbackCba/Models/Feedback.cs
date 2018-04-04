using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackCba.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime SubmitDate { get; set; }
        public decimal Score{ get; set; }
        public string PageUrl { get; set; }
        public bool IsMainPage { get; set; }
        public string Answer { get; set; }
    }
}