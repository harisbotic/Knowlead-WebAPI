using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserLanguageModel
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        [Required]
        public int LanguageId { get; set; }
        public LanguageModel Language { get; set; }
    }
}