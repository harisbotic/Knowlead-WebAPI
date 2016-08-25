using System;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
  public class ApplicationUserSkill
  {
      public Guid ApplicationUserId { get; set; }
      public ApplicationUser ApplicationUser { get; set; }

      public int FosId { get; set; }
      public FOS Fos { get; set; }
  }
}