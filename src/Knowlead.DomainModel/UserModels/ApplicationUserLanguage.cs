using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserLanguage : EntityBase
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [MyRequired]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}