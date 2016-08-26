using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowlead.DomainModel.LookupModels.Geo
{
    [Table("GeoLookups")]
    public class _GeoLookup
    {
        [Key]
        public int GeoLookupId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}