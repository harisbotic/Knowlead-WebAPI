using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.FeedbackModels;

namespace Knowlead.DomainModel.LookupModels.FeedbackModels
{
    public class FeedbackP2P : _Feedback
    {
        [Range(0, 5)]
        [Required]
        public float Knowleadge { get; set; }
        [Range(0, 5)]
        [Required]
        public float Accurate { get; set; }
    }
}