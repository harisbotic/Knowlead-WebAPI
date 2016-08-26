using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.UserModels
{
    public class UserCertificate
    {
        [Key]
        public int UserCertificateId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
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