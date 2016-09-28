using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.LookupModels.Geo;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean? IsMale { get; set; }
        public string Timezone { get; set; } //TimezoneInfo.Id
        public string AboutMe { get; set; }


        public int? CountryId { get; set; }
        public CountryModel Country { get; set; }
        
        public int? StateId { get; set; }
        public StateModel State { get; set; }
        
        [Required]
        public UserStatus Status { get; set; }

        public enum UserStatus 
        {
            Online, Offline, Busy
        }

        public ICollection<ApplicationUserLanguageModel> ApplicationUserLanguages { get; set; }
        
        public ApplicationUserModel()
        {
            this.Status = UserStatus.Offline;
        }
    }
}