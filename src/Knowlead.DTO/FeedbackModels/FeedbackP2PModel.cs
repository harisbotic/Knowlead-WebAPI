using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.FeedbackModels;

namespace Knowlead.DTO.LookupModels.FeedbackModels
{
    public class FeedbackP2PModel : _FeedbackModel
    {
        [Range(0, 5)]
        [Required]
        public float Knowleadge { get; set; }
        [Range(0, 5)]
        [Required]
        public float Accurate { get; set; }
    }
}