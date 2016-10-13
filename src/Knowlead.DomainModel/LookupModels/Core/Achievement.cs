using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.LookupModels.Core
{
    public class Achievement : _CoreLookup
    {
        [MyRequired]
        public string Desc { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}