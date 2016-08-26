using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserSkill
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int FosId { get; set; }
        public FOS Fos { get; set; }
    }
}