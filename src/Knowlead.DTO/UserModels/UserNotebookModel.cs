using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.UserModels
{
    public class UserNotebookModel
    {
        [MyRequired]
        public int UserNotebookId { get; set; }
        [MyRequired]
        public string Name { get; set; }
        [MyRequired]
        public string Markdown { get; set; }
        [MyRequired]
        public DateTime CreatedAt { get; set; }


        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        public Guid ImageId { get; set; }
        public ImageModel Image { get; set; }

        public UserNotebookModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}