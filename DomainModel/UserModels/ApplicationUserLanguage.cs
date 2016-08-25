using System;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
  public class ApplicationUserLanguage
  {
      public Guid ApplicationUserId { get; set; }
      public ApplicationUser ApplicationUser { get; set; }

      public int LanguageId { get; set; }
      public Language Language { get; set; }
  }
}