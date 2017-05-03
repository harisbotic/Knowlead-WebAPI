using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.Common.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.Auth.Hax
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

        public int? CountryId { get; set; }
        
        public int? StateId { get; set; }
        
        public int? MotherTongueId { get; set; }


        [MyRequired]
        public UserStatus Status { get; set; }
        
        public ApplicationUser()
        {
            this.Status = UserStatus.Offline;
        }
    }
}