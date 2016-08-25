using System;
using System.Collections.Generic;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.LookupModels.Geo;
using OpenIddict;

namespace Knowlead.DomainModel.UserModels
{
  public class ApplicationUser : OpenIddictUser<Guid>
  {
    public string Timezone { get; set; } //TimezoneInfo.Id
    public string AboutMe { get; set; }

    public int CityId { get; set; }
    public City City { get; set; }
    
    public int StatusId { get; set; }
    public Status Status { get; set; }


    public ICollection<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
    
  }
}