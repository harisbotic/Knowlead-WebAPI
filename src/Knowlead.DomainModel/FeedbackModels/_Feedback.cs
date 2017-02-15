using System;
using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Knowlead.DomainModel.FeedbackModels
{
    [Table("Feedbacks")]
    public abstract class _Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        
        [MyRequired]
        public String FeedbackText { get; set; }

        public String TeacherReply { get; set; }

        [MyRequired]
        public int FosId { get; set; }
        public FOS Fos { get; set; }

        [MyRequired]
        public Guid TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }

        [MyRequired]
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public float Rating { get; set; }

        public abstract void CalculateRating();
        public abstract Dictionary<string, int> GetRatingParameters();
    }
}