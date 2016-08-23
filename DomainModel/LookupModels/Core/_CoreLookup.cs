using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowlead.DomainModel.LookupModels.Core
{
    [Table("CoreLookup")]
    public class _CoreLookup
    {
        [Key]
        public int CoreLookupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}