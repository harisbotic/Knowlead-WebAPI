using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.UserModels
{
    public class UserCertificate
    {
        [Key]
        public int UserCertificateId { get; set; }
        [MyRequired]
        public string Name { get; set; }
        public string Desc { get; set; }

        [MyRequired]
        public DateTime CreatedAt { get; set; }

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }

        public UserCertificate()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}