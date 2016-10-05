using Knowlead.Common.DataAnnotations;

namespace Knowlead.DomainModel.LookupModels.Geo
{
    public class State : _GeoLookup
    {
        [MyRequired]
        public int StatesCountryId { get; set; }
        public Country StatesCountry { get; set; }
    }
}