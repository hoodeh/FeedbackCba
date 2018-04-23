using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FeedbackCba.Core.Models
{
    public class Customer
    {
        public Customer()
        {
            Questions = new Collection<Question>();    
        }

        public Guid Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string BgColor { get; set; }

        public string Statement { get; set; }
        public string AppLevelQuestion { get; set; }
        public string PageLevelQuestion { get; set; }
        public string ValidDomains { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<Question> Questions { get; set; }

        public bool IsValid()
        {
            return IsEnabled && ExpireDate >= DateTime.Now;
        }
    }
}