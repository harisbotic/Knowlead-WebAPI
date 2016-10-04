using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserLanguageModel
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        [MyRequired]
        public int LanguageId { get; set; }
        public LanguageModel Language { get; set; }
    }
}