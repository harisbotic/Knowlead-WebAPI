using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.BlobModels
{
    public class UploadedFileModel
    {
        [MyRequired]
        public Guid Filename { get; set; }
        public long Filesize { get; set; }

        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUserModel UploadedBy { get; set; }
    }
}