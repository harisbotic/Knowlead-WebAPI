using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.StatisticsModels
{
    public class PlatformFeedbackModel
    {
        [MyRequired]
        public String Text { get; set; }
    }
}