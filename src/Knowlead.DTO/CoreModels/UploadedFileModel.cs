using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.CoreModels
{
    public class UploadedFileModel
    {
        [Required]
        public Guid Filename { get; set; }
        public long Filesize { get; set; }

        [Required]
        public Guid UploadedById { get; set; }
        public ApplicationUserModel UploadedBy { get; set; }
    }
}