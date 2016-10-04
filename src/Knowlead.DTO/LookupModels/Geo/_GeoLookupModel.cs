using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Geo
{
    public class _GeoLookupModel
    {
        public int GeoLookupId { get; set; }
        [MyRequired]
        public string Code { get; set; }
        [MyRequired]
        public string Name { get; set; }
    }
}