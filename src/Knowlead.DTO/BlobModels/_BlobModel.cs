using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.BlobModels
{
    public class _BlobModel : EntityBaseModel
    {
        [MyRequired]
        public Guid BlobId { get; set; }
        public string BlobType { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public long Filesize { get; set; }

        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUserModel UploadedBy { get; set; }

        public _BlobModel()
        {   
        }
    }
}