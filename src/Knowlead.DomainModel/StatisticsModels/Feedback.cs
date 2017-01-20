using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.StatisticsModels
{
    public class PlatformFeedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public Guid SubmittedById { get; set; }
        public String Text { get; set; }
        public ApplicationUser SubmittedBy { get; set; }
    }
}