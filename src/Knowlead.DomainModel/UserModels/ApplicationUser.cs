using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [MyRequired]
        [MyEmailAddress]
        override public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean? IsMale { get; set; }
        public string Timezone { get; set; }
        public string AboutMe { get; set; }
        public float AverageRating { get; set; }
        
        [NotMapped]
        public int? MinutesBalance { get; set; }
        [NotMapped]
        public int? PointsBalance { get; set; }


        public Guid? ProfilePictureId { get; set; }
        public ImageBlob ProfilePicture { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }
        
        public int? StateId { get; set; }
        public State State { get; set; }
        
        public int? MotherTongueId { get; set; }
        [ForeignKey("MotherTongueId")]
        public Language MotherTongue { get; set; }

        public ICollection<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public ICollection<ApplicationUserInterest> ApplicationUserInterests { get; set; }

        [MyRequired]
        public UserStatus Status { get; set; }
        
        public ApplicationUser()
        {
            this.Status = UserStatus.Offline;
            this.ApplicationUserLanguages = new List<ApplicationUserLanguage>();
            this.ApplicationUserInterests = new List<ApplicationUserInterest>();
        }
    }
}