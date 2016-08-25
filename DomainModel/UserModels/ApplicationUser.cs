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

    public int? CityId { get; set; }
    public City City { get; set; }
    
    public UserStatus Status { get; set; } = UserStatus.Offline;

    public enum UserStatus {
      Online, Offline, Busy
    }

    public ICollection<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
    
  }
}