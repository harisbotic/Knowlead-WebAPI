using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO {
    public class RegisterUserModel {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}