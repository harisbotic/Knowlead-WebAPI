using AutoMapper;
using Knowlead.DomainModel.CoreModels;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.UserModels
{
    public class _CoreProfile : Profile
    {
        public _CoreProfile()
        {
            CreateMap<Image, ImageModel>().ReverseMap();

            CreateMap<UploadedFile, UploadedFileModel>().ReverseMap();
        }
    }
}
