using AutoMapper;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DTO.LookupModels.Geo;

namespace Knowlead.DTO.UserModels
{
    public class _GeoLookupProfile : Profile
    {
        public _GeoLookupProfile()
        {
            CreateMap<State, StateModel>().ReverseMap();

            CreateMap<Country, CountryModel>().ReverseMap();
           

        }
    }
}
