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
            CreateMap<IFormFile, ImageBlob>()
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetExtension(src.FileName)))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.FileName)));

            CreateMap<FileBlob, FileBlobModel>().ReverseMap();
            CreateMap<IFormFile, FileBlob>()
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetExtension(src.FileName)))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.FileName)));
        
        }
        private string GetExtension(string filename)
        {
            var extension = Path.GetExtension(filename);
            if(!string.IsNullOrWhiteSpace(extension))
                return extension.Substring(1);
            else
                return "file";
        }
    }
}

