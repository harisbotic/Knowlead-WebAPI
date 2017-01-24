using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.BlobModels;

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
        public Guid CreatedById { get; set; }
        public ApplicationUserModel CreatedBy { get; set; }

        public Guid ImageBlobId { get; set; }
        public ImageBlobModel ImageBlob { get; set; }

        public UserNotebookModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}