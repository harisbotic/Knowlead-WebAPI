using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
  public class ApplicationUserLanguage
  {
      [Required]
      public Guid ApplicationUserId { get; set; }
      public ApplicationUser ApplicationUser { get; set; }

      [Required]
      public int LanguageId { get; set; }
      public Language Language { get; set; }
  }
}