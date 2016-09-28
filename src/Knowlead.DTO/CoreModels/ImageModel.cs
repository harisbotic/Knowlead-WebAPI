using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.CoreModels
{
    public class ImageModel
    {
        [Required]
        public Guid Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [Required]
        public Guid UploadedById { get; set; }
        public ApplicationUserModel UploadedBy { get; set; }
    }
}

