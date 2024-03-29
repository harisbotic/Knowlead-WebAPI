using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.LibraryModels
{
    public class NotebookModel : EntityBaseModel
    {
        [MyRequired]
        public int NotebookId { get; set; }
        [MyRequired]
        public String Name { get; set; }
        public String Markdown { get; set; }
        [MyRequired]
        public String PrimaryColor { get; set; } = "#071923";
        [MyRequired]
        public String SecondaryColor { get; set; } = "#007bb6";
        public bool IsDeleted { get; set; } = false;


        [MyRequired]
        public Guid CreatedById { get; set; }
        public ApplicationUserModel CreatedBy { get; set; }
    }
}