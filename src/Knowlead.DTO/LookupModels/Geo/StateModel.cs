using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.LookupModels.Geo
{
    public class StateModel : _GeoLookupModel
    {
        [MyRequired]
        public int StatesCountryId { get; set; }
        public CountryModel StatesCountry { get; set; }
    }
}