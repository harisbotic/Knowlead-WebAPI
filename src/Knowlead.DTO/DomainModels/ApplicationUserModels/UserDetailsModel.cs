using System;
using Knowlead.DTO.DomainModels.LookupModels.Core;
using Knowlead.DTO.DomainModels.LookupModels.Geo;

namespace Knowlead.DTO.DomainModels.ApplicationUserModels
{
    public class UserDetailsModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean? IsMale { get; set; }

        public int? NativLanguageId { get; set; }
        public LanguageModel NativLanguage { get; set; }

        public int? CountryId { get; set; }
        public CountryModel Country { get; set; }

        public int? StateId { get; set; }
        public StateModel State { get; set; }
    }
}