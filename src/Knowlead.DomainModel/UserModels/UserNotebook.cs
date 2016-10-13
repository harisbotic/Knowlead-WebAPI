using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.UserModels
{
    public class UserNotebook
    {
        [Key]
        public int UserNotebookId { get; set; }
        [MyRequired]
        public string Name { get; set; }
        [MyRequired]
        public string Markdown { get; set; }
        [MyRequired]
        public DateTime CreatedAt { get; set; }


        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }

        public UserNotebook()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}