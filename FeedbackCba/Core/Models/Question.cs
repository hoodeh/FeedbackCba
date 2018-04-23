using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackCba.Core.Models
{
    public enum QuestionType
    {
        Text = 1,
        CheckBox = 2
    }

    public class Question
    {
        public int Id { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        [MaxLength(255)]
        public string Text { get; set; }

        public QuestionType Type { get; set; }
        public short FromRate { get; set; }
        public short ToRate { get; set; }
        public bool IsEnabled { get; set; }
    }
}