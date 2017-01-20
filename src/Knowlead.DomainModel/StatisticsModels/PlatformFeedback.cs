using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.StatisticsModels
{
    public class PlatformFeedback
    {
        [Key]
        public Guid PlatformFeedbackId { get; set; }
        public String Feedback { get; set; }
        
        public ApplicationUser SubmittedBy { get; set; }
        public Guid SubmittedById { get; set; }
    }
}