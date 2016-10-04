using System;
using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Geo;
using OpenIddict;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUser : OpenIddictUser<Guid>
    {
        [MyRequired]
        [MyEmailAddress]
        override public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean? IsMale { get; set; }
        public string Timezone { get; set; } //TimezoneInfo.Id
        public string AboutMe { get; set; }


        public int? CountryId { get; set; }
        public Country Country { get; set; }
        
        public int? StateId { get; set; }
        public State State { get; set; }
        
        [MyRequired]
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