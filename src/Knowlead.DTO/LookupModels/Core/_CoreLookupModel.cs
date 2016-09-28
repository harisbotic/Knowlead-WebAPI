using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Core
{
    public class _CoreLookupModel
    {
        public int CoreLookupId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}