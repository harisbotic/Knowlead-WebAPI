using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.BlobModels
{
    public class _Blob : EntityBase
    {
        [Key]
        public Guid BlobId { get; set; }
        public string BlobType { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public long Filesize { get; set; }


        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }

        public _Blob()
        {
            this.BlobId = Guid.NewGuid();
        }
        public _Blob(string blobType) : this()
        {
            BlobType = blobType;
        }
    }
}

