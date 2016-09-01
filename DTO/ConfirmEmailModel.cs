using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO {
    public class ConfirmEmailModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Password { get; set; }
    }
}