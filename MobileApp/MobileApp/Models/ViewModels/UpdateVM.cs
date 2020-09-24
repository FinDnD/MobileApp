using System.ComponentModel.DataAnnotations;

namespace MobileApp.Models.ViewModels
{
    class UpdateVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
