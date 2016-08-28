using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Geo;
using OpenIddict;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUser : OpenIddictUser<Guid>
    {
        [Required]
        [EmailAddress]
        override public string Email { get; set; }
        public string Timezone { get; set; } //TimezoneInfo.Id
        public string AboutMe { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }
        
        [Required]
        public UserStatus Status { get; set; }

        public enum UserStatus 
        {
            Online, Offline, Busy
        }

        public ICollection<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        
        public ApplicationUser()
        {
            this.Status = UserStatus.Offline;
        }
    }
}