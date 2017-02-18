using AutoMapper;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.DTO.LibraryModels
{
    public class _LibraryProfile : Profile
    {
        public _LibraryProfile()
        {
            CreateMap<NotebookModel, Notebook>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PrimaryColor, opt => opt.MapFrom(src => src.PrimaryColor))
                .ForMember(dest => dest.SecondaryColor, opt => opt.MapFrom(src => src.SecondaryColor))
                .ForMember(dest => dest.Markdown, opt => opt.MapFrom(src => src.Markdown))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Notebook, NotebookModel>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<StickyNoteModel, StickyNote>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NoteText, opt => opt.MapFrom(src => src.NoteText))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<StickyNote, StickyNoteModel>()
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

        }
    } 
}
