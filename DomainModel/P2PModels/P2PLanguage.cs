using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PLangugage
    {
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
      
    }
}