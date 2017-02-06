using AutoMapper;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.DTO.LibraryModels
{
    public class _LibraryProfile : Profile
    {
        public _LibraryProfile()
        {
            CreateMap<NotebookModel, Notebook>()
                .ForMember(dest => dest.NotebookId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateNotebookModel, Notebook>();
        }
    } 
}
