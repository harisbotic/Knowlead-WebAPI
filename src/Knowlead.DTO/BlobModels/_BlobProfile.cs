using System.IO;
using AutoMapper;
using Knowlead.DomainModel.BlobModels;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DTO.BlobModels;
using Microsoft.AspNetCore.Http;

namespace Knowlead.DTO.UserModels
{
    public class _BlobProfile : Profile
    {
        public _BlobProfile()
        {
            CreateMap<ImageBlob, ImageBlobModel>()
            .ForMember(dest => dest.UploadedBy, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<IFormFile, ImageBlob>()
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetExtension(src.FileName)))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.FileName)));

            CreateMap<FileBlob, FileBlobModel>()
            .ForMember(dest => dest.UploadedBy, opt => opt.Ignore())
            .ReverseMap();
            CreateMap<IFormFile, FileBlob>()
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => GetExtension(src.FileName)))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => Path.GetFileNameWithoutExtension(src.FileName)));

            CreateMap<P2PImage, _BlobModel>()
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.ImageBlobId))
                .ForMember(dest => dest.BlobType, opt => opt.MapFrom(src => src.ImageBlob.BlobType))
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.ImageBlob.Filesize))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.ImageBlob.Filename))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.ImageBlob.Extension))
                .ForMember(dest => dest.UploadedById, opt => opt.MapFrom(src => src.ImageBlob.UploadedById));
            
            CreateMap<P2PFile, _BlobModel>()
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.FileBlobId))
                .ForMember(dest => dest.BlobType, opt => opt.MapFrom(src => src.FileBlob.BlobType))
                .ForMember(dest => dest.Filesize, opt => opt.MapFrom(src => src.FileBlob.Filesize))
                .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.FileBlob.Filename))
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.FileBlob.Extension))
                .ForMember(dest => dest.UploadedById, opt => opt.MapFrom(src => src.FileBlob.UploadedById));
    

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

