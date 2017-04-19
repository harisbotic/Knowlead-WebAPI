using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.LibraryModels
{
    public class StickyNote : EntityBase
    {
        [MyRequired]
        public int StickyNoteId { get; set; }
        [MyRequired]
        public String Name { get; set; }
        public String NoteText { get; set; }

        [MyRequired]
        public bool IsDeleted { get; set; } = false;


        [MyRequired]
        public Guid CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

    }
}