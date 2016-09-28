using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.UserModels
{
    public class UserCertificateModel
    {
        [Required]
        public int UserCertificateId { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        public int ImageId { get; set; }
        public ImageModel Image { get; set; }

        public UserCertificateModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}