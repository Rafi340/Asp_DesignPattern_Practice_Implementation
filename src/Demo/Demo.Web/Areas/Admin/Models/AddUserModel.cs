using System.ComponentModel.DataAnnotations;

namespace Demo.Web.Areas.Admin.Models
{
    public class AddUserModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
