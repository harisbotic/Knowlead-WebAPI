using System;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;

namespace Knowlead.DTO.ApplicationUserModels
{
    public class UserDetailsModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AboutMe { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean? IsMale { get; set; }

        public int? NativLanguageId { get; set; }
        public Language NativLanguage { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public int? StateId { get; set; }
        public State State { get; set; }
    }
}