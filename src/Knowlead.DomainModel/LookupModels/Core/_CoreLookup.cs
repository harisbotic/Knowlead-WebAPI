using Knowlead.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Knowlead.DomainModel.LookupModels.Core
{
    [Table("CoreLookups")]
    public class _CoreLookup
    {
        [Key]
        public int CoreLookupId { get; set; }
        [MyRequired]
        public string Code { get; set; }
        [MyRequired]
        public string Name { get; set; }
    }
}