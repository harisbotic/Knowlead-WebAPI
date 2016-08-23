using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.UserModels
{
    public class UserNotebook
    {
        [Key]
        public int UserNotebookId { get; set; }
        public string Name { get; set; }
        public string Markdown { get; set; }
        public DateTime CreatedAt { get; set; }


        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public UserNotebook()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}