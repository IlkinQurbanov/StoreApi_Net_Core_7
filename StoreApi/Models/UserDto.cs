using System.ComponentModel.DataAnnotations;

namespace StoreApi.Models
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; } = "";
        [Required(ErrorMessage ="Please provide your last Name")]
        public string LastName { get; set; } = "";
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        [Required]
        [MinLength(6, ErrorMessage ="The Address should be at least 5 characters")]
        [MaxLength(1000, ErrorMessage ="The Address should br least than 1000 characters")]
        public string Address { get; set; } = "";
    }
}
