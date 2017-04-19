using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.LibraryModels
{
    public class StickyNoteModel : EntityBaseModel
    {
        public int StickyNoteId { get; set; }

        [MyRequired]
        public String Name { get; set; }
        public String NoteText { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Guid CreatedById { get; set; }
        public ApplicationUserModel CreatedBy { get; set; }
    }
}