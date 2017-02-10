using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DTO.BlobModels;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.UserModels;
using static Knowlead.Common.Constants.EnumStatuses;

namespace Knowlead.DTO.P2PModels
{
    public class P2PModel
    {        
        [MyRequired]
        public int P2pId { get; set; }
        
        [MyRequired]
        public string Title { get; set; }      
          
        [MyRequired]
        public string Text { get; set; }
        
        [MyRequired]
        public int InitialPrice { get; set; }
        
        public DateTime? Deadline { get; set; }
        
        public int? PriceAgreed { get; set; }
        public DateTime? DateTimeAgreed { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? ScheduledWithId { get; set; }
        [ForeignKey("ScheduledWithId")]
        public ApplicationUserModel ScheduledWith { get; set; }


        public Guid? CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public ApplicationUserModel CreatedBy { get; set; }


        public List<LanguageModel> Languages { get; set; }
        public List<_BlobModel> Blobs { get; set; }
        public List<P2PMessageModel> P2pMessageModels { get; set; }
        
        [MyRequired]
        public int FosId { get; set; }
        public FOSModel Fos { get; set; }

        [MyRequired]
        public P2PStatus Status { get; set; }

        public P2PModel()
        {
            this.Status = P2PStatus.Inactive;
            this.Languages = new List<LanguageModel>();
            this.Blobs = new List<_BlobModel>();
        }
    }
}