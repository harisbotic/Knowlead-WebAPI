using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserSkillModel
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        [MyRequired]
        public int FosId { get; set; }
        public FOSModel Fos { get; set; }
    }
}