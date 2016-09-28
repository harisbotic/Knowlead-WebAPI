using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.FeedbackModels
{
    public class _FeedbackModel
    {
        public string FeedbackText { get; set; }

        [Required]
        public int FosId { get; set; }
        public FOSModel Fos { get; set; }

        [Required]
        public Guid TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public ApplicationUserModel Teacher { get; set; }

        [Required]
        public Guid StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUserModel Student { get; set; }
    }
}