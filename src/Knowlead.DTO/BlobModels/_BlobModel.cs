using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.BlobModels
{
    public class _BlobModel
    {
        [MyRequired]
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }

        [MyRequired]
        public Guid UploadedById { get; set; }
        public ApplicationUserModel UploadedBy { get; set; }

        public _BlobModel()
        {
            this.Id = Guid.NewGuid();
        }
    }
}