using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.BlobModels;

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
        public Guid CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public Guid ImageBlobId { get; set; }
        public ImageBlob ImageBlob { get; set; }

        public UserNotebook()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}