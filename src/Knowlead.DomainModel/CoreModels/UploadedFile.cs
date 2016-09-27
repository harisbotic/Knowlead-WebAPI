using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.CoreModels
{
    public class UploadedFile
    {
        [Key]
        public Guid Filename { get; set; }
        public long Filesize { get; set; }

        [Required]
        public Guid UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }
    }
}