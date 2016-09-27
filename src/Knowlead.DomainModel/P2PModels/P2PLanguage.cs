using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PLangugage
    {
        [Required]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [Required]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
      
    }
}