using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.BlobModels
{
    public class FileBlob
    {
        [Key]
        public Guid Filename { get; set; }
        public long Filesize { get; set; }

        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }
    }
}