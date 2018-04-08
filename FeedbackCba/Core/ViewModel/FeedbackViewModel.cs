using System;
using System.ComponentModel.DataAnnotations;

namespace FeedbackCba.Core.ViewModel
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public bool IsMainPage { get; set; }
        public string Answer { get; set; }
        public decimal Score { get; set; }
        public DateTime SubmitDate { get; set; }
        public string UserId { get; set; }
        
        [Display(Name = "Name")]
        public string UserName { get; set; }
        
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        public string Question
        {
            get { return IsMainPage ? "How do you like this site?" : "How helpful is this page?"; }
        }

        public bool HasValidResult
        {
            get { return Id > 0 && SubmitDate.AddDays(180) > DateTime.Now; }
        }
    }
}