using System;
using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.LibraryModels
{
    public class CreateNotebookModel
    {
        [MyRequired]
        public String Name { get; set; }
        [MyRequired]
        public String PrimaryColor { get; set; } = "#071923";
        [MyRequired]
        public String SecondaryColor { get; set; } = "#007bb6";
    }
}
