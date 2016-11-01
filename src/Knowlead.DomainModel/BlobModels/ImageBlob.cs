using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.BlobModels
{
    public class ImageBlob
    {
        [Key]
        public Guid Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }
    }
}
