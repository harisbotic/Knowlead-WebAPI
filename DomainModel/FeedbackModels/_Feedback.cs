using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.FeedbackModels
{
    [Table("Feedback")]
    public class _Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public string FeedbackText { get; set; }

        public int FosId { get; set; }
        public FOS Fos { get; set; }
        
        //public int TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }
        
        //public int StudentId { get; set; }
        public ApplicationUser Student { get; set; }
    }
}