using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Core
{
    public class FOSModel : _CoreLookupModel
    {
        [MyRequired]
        public string FosDesc { get; set; }
    }
}