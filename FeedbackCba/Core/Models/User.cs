
using System.ComponentModel.DataAnnotations;

namespace FeedbackCba.Core.Models
{
    public class User
    {
        [Key]
        [StringLength(128)]
        public string Guid { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}