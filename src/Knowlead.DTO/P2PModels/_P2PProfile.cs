using System.Collections.Generic;
using AutoMapper;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DTO.BlobModels;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.P2PModels;

namespace Knowlead.DTO.UserModels
{
    public class _P2PProfile : Profile
    {
        public _P2PProfile()
        {
            CreateMap<P2PModel, P2P>()
                .ForMember(dest => dest.P2pId, opt => opt.Ignore())
                .ForMember(dest => dest.P2PLanguages, opt => opt.Ignore())
                .ForMember(dest => dest.Fos, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedById, opt => opt.Ignore());

            CreateMap<P2P, P2PModel>()
                .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.P2PLanguages))
                .ForMember(dest => dest.Blobs, opt => opt.MapFrom(src => src.P2PFiles))
                .AfterMap((p, m) => m.Blobs.AddRange(Mapper.Map<List<_BlobModel>>(p.P2PImages)));
                
            CreateMap<P2PLanguageModel, LanguageModel>()
                .ForMember(dest => dest.CoreLookupId, opt => opt.MapFrom(src => src.LanguageId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Language.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Language.Name));

            CreateMap<P2PMessage, P2PMessageModel>();

            CreateMap<P2PMessageModel, P2PMessage>()
                .ForMember(dest => dest.MessageFromId, opt => opt.Ignore())
                .ForMember(dest => dest.MessageFrom, opt => opt.Ignore())
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore())
                .ForMember(dest => dest.P2pMessageId, opt => opt.Ignore())
                .ForMember(dest => dest.P2p, opt => opt.Ignore());

            CreateMap<P2PFileModel, FileBlobModel>()
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.FileBlobId));

            CreateMap<P2PImageModel, ImageBlobModel>()
                .ForMember(dest => dest.BlobId, opt => opt.MapFrom(src => src.ImageBlobId));

            CreateMap<P2PLanguage, LanguageModel>()
                .ForMember(dest => dest.CoreLookupId, opt => opt.MapFrom(src => src.LanguageId));
            //Fill in
        }
    }
}
