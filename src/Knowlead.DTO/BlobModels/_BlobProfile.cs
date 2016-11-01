using AutoMapper;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DTO.BlobModels;

namespace Knowlead.DTO.UserModels
{
    public class _BlobProfile : Profile
    {
        public _BlobProfile()
        {
            CreateMap<ImageBlob, ImageBlobModel>().ReverseMap();

            CreateMap<FileBlob, FileBlobModel>().ReverseMap();
        }
    }
}
