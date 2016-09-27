using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.DomainModels.LookupModels.Core
{
    public class _CoreLookupModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}