using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Core
{
    public class FOSModel : _CoreLookupModel
    {
        public FOSModel ParentFos { get; set; }
        
        public int? ParentFosId { get; set; }
    }
}