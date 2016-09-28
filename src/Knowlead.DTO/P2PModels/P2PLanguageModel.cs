using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.P2PModels
{
    public class P2PLangugageModel
    {
        [Required]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [Required]
        public int LanguageId { get; set; }
        public LanguageModel Language { get; set; }
      
    }
}