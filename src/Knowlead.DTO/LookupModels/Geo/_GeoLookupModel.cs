using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Geo
{
    public class _GeoLookupModel
    {
        public int GeoLookupId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}