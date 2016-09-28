using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.UserModels
{
    public class UserNotebookModel
    {
        [Required]
        public int UserNotebookId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Markdown { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }


        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        public int ImageId { get; set; }
        public ImageModel Image { get; set; }

        public UserNotebookModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}