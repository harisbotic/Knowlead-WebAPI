using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Knowlead.DomainModel.LookupModels.Geo
{
    [Table("GeoLookups")]
    public class _GeoLookup
    {
        [Key]
        public int GeoLookupId { get; set; }
        [MyRequired]
        public string Code { get; set; }
        [MyRequired]
        public string Name { get; set; }
    }
}