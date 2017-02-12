using System;
using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DomainModel.P2PModels
{
    [Table("P2Ps")]
    public class P2P
    {
        [Key]
        public int P2pId { get; set; }
        
        [MyRequired]
        public string Text { get; set; }
        
        [MyRequired]
        public int InitialPrice { get; set; }
        
        public DateTime? Deadline { get; set; }
        
        public int? PriceAgreed { get; set; }
        public DateTime? DateTimeAgreed { get; set; }

        public bool IsDeleted { get; set; }

        [MyRequired]
        public DateTime DateCreated { get; set; }

        public Guid? ScheduledWithId { get; set; }
        [ForeignKey("ScheduledWithId")]
        public ApplicationUser ScheduledWith { get; set; }

        [MyRequired]
        public Guid CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public ApplicationUser CreatedBy { get; set; }
        

        public ICollection<P2PLanguage> P2PLanguages { get; set; }
        public ICollection<P2PImage> P2PImages { get; set; }
        public ICollection<P2PFile> P2PFiles { get; set; }

        
        [MyRequired]
        public int FosId { get; set; }
        public FOS Fos { get; set; }
        
        [MyRequired]
        public P2PStatus Status { get; set; }

        public P2P()
        {
            this.DateCreated = DateTime.UtcNow;
            this.Status = P2PStatus.Inactive;
            this.P2PLanguages = new List<P2PLanguage>();
            this.P2PImages = new List<P2PImage>();
            this.P2PFiles = new List<P2PFile>();
        }
    }
}