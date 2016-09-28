using AutoMapper;
using Knowlead.DomainModel.CoreModels;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.UserModels
{
    public class _CoreProfile : Profile
    {
        public _CoreProfile()
        {
            CreateMap<Image, ImageModel>();
            CreateMap<ImageModel, Image>();

            CreateMap<UploadedFile, UploadedFileModel>();
            CreateMap<UploadedFileModel, UploadedFile>();
        }
    }
}
