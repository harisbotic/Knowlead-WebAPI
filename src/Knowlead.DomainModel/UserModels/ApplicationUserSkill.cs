using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserSkill
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [MyRequired]
        public int FosId { get; set; }
        public FOS Fos { get; set; }
    }
}