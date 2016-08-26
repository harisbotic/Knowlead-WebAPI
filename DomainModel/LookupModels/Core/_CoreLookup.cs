using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowlead.DomainModel.LookupModels.Core
{
    [Table("CoreLookups")]
    public class _CoreLookup
    {
        [Key]
        public int CoreLookupId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}