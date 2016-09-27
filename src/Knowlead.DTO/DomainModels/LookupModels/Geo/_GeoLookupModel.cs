using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.DomainModels.LookupModels.Geo
{
    public class _GeoLookupModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}