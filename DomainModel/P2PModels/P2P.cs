using System;
using System.ComponentModel.DataAnnotations.Schema;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.P2PModels
{
    [Table("P2Ps")]
    public class P2P
    {
        public int P2pId { get; set; }
        public float Rate { get; set; }
        public DateTime StartingAt { get; set; } // not required
        
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}