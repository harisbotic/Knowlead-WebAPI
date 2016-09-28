using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserSkillModel
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        [Required]
        public int FosId { get; set; }
        public FOSModel Fos { get; set; }
    }
}