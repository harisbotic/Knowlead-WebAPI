using System;
using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;
using System.ComponentModel.DataAnnotations;

namespace Knowlead.DomainModel.FeedbackModels
{
    [Table("Feedbacks")]
    public class _Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public string FeedbackText { get; set; }

        [MyRequired]
        public int FosId { get; set; }
        public FOS Fos { get; set; }

        [MyRequired]
        public Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUser Teacher { get; set; }

        [MyRequired]
        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }
    }
}