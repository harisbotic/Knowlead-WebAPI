using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserInterest : EntityBase
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [MyRequired]
        public int FosId { get; set; }
        public FOS Fos { get; set; }

        [Range(0,5)]
        public int Stars { get; set; }
    }
}