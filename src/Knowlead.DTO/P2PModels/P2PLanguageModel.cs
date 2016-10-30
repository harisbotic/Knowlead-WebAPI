using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.P2PModels
{
    public class P2PLanguageModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [MyRequired]
        public int LanguageId { get; set; }
        public LanguageModel Language { get; set; }
      
    }
}