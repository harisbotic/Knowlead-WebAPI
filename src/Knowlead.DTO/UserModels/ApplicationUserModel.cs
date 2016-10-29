using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.LookupModels.Geo;

namespace Knowlead.DTO.UserModels
{
    public class ApplicationUserModel
    {
        [MyRequired]
        [MyEmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean? IsMale { get; set; }
        public string Timezone { get; set; }
        public string AboutMe { get; set; }


        public int? CountryId { get; set; }
        public CountryModel Country { get; set; }
        
        public int? StateId { get; set; }
        public StateModel State { get; set; }
    
        public int? MotherTongueId { get; set; }
        [ForeignKey("MotherTongueId")]
        public LanguageModel MotherTongue { get; set; }

        public List<LanguageModel> Languages { get; set; }
        public List<InterestModel> Interests { get; set; }


        [MyRequired]
        public UserStatus Status { get; set; }

        public enum UserStatus 
        {
            Online, Offline, Busy
        }
        
        public ApplicationUserModel()
        {
            this.Status = UserStatus.Offline;
            this.Languages = new List<LanguageModel>();
            this.Interests = new List<InterestModel>();
        }
    }
}