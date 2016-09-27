using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FeedbackModels
{
    [Table("Feedbacks")]
    public class _Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public string FeedbackText { get; set; }

        [Required]
        public int FosId { get; set; }
        public FOS Fos { get; set; }

        [Required]
        public Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }

        [Required]
        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }
    }
}