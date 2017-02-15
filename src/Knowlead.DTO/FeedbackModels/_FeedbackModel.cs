using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.FeedbackModels
{
    public class _FeedbackModel
    {
        public int FeedbackId { get; set; }

        [MyRequired]
        public String FeedbackText { get; set; }

        public String TeacherReply { get; set; }

        public int? FosId { get; set; }
        public FOSModel Fos { get; set; }

        public Guid TeacherId { get; set; }
        public ApplicationUserModel Teacher { get; set; }

        public Guid StudentId { get; set; }
        public ApplicationUserModel Student { get; set; }
    }
}