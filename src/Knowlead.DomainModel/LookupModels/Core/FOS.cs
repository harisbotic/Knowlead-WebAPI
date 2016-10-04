using Knowlead.Common.DataAnnotations;

namespace Knowlead.DomainModel.LookupModels.Core
{
    public class FOS : _CoreLookup
    {
        [MyRequired]
        public string FosDesc { get; set; }
    }
}