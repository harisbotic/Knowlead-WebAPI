using System.Collections.Generic;

namespace Knowlead.DTO.LookupModels.Core
{
    public class FOSModel : _CoreLookupModel
    {
        // public FOSModel ParentFos { get; set; }  removed for DTO only
        public int? ParentFosId { get; set; }
        public ICollection<FOSModel> Children;
    }
}