using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Knowlead.DomainModel.UserModels
{
  public class ApplicationUser : IdentityUser
  {
    public string Timezone { get; set; } //TimezoneInfo.Id
    public string AboutMe { get; set; }

    //city_id
    //availability_id
    
  }
}