using System.IO;
using AutoMapper;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DTO.BlobModels;
using Microsoft.AspNetCore.Http;

namespace Knowlead.DTO.UserModels
{
    public class _BlobProfile : Profile
    {
        public _BlobProfile()
        {
            CreateMap<ImageBlob, ImageBlobModel>().ReverseMap();
            CreateMap<IFormFile, ImageBlobModel>()
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetExtension(src.FileName)))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.FileName)));

            CreateMap<FileBlob, FileBlobModel>().ReverseMap();
            CreateMap<IFormFile, FileBlobModel>()
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.FileName)))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetExtension(src.FileName)));
        
        }
        private string GetExtension(string filename) => Path.GetExtension(filename).Substring(1);
    }
}

