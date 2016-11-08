using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.BlobModels
{
    public class _Blob
    {
        [Key]
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }


        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUser UploadedBy { get; set; }

        public _Blob()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

