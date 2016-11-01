using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.BlobModels;

namespace Knowlead.DTO.UserModels
{
    public class UserCertificateModel
    {
        [MyRequired]
        public int UserCertificateId { get; set; }
        
        [MyRequired]
        public string Name { get; set; }
        public string Desc { get; set; }

        [MyRequired]
        public DateTime CreatedAt { get; set; }

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        public Guid ImageBlobId { get; set; }
        public ImageBlobModel ImageBlob { get; set; }

        public UserCertificateModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}