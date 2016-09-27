using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.SpecificModels.ApplicationUserModels
{
    public class RegisterUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}